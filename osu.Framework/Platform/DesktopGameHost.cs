﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using osu.Framework.Development;
using osu.Framework.Input;
using osu.Framework.Input.Handlers;
using osu.Framework.Input.Handlers.Joystick;
using osu.Framework.Input.Handlers.Keyboard;
using osu.Framework.Input.Handlers.Mouse;
using osu.Framework.Logging;
using osuTK;

namespace osu.Framework.Platform
{
    public abstract class DesktopGameHost : GameHost
    {
        private Mutex mutex;
        private readonly bool allowMultipleInstances;

        private TcpIpcProvider ipcProvider;
        private readonly bool bindIPCPort;
        private Thread ipcThread;

        protected DesktopGameHost(string gameName = @"", bool allowMultipleInstances = true, bool bindIPCPort = false, ToolkitOptions toolkitOptions = default, bool portableInstallation = false)
            : base(gameName, toolkitOptions)
        {
            this.allowMultipleInstances = allowMultipleInstances;
            this.bindIPCPort = bindIPCPort;
            IsPortableInstallation = portableInstallation;
        }

        protected override void SetupForRun()
        {
            if (!allowMultipleInstances)
            {
                var manifestModuleName = DebugUtils.HostAssembly.ManifestModule.Name;
                mutex = new Mutex(true, $"Global\\{manifestModuleName}", out var createdNew);
                if (!createdNew)
                    throw new InvalidOperationException($"Only one instance of {manifestModuleName} is allowed");

                CleanupRequested += () => mutex?.ReleaseMutex();
            }

            //todo: yeah.
            Architecture.SetIncludePath();

            Logger.Storage = Storage.GetStorageForDirectory("logs");

            if (bindIPCPort)
                startIPC();

            base.SetupForRun();
        }

        private void startIPC()
        {
            Debug.Assert(ipcProvider == null);

            ipcProvider = new TcpIpcProvider();
            IsListeningIpc = ipcProvider.Bind();

            if (IsListeningIpc)
            {
                ipcProvider.MessageReceived += OnMessageReceived;

                ipcThread = new Thread(() => ipcProvider.StartAsync().Wait())
                {
                    Name = "IPC",
                    IsBackground = true
                };

                ipcThread.Start();
            }
        }

        public bool IsPortableInstallation { get; }

        public override void OpenFileExternally(string filename) => openUsingShellExecute(filename);

        public override void OpenUrlExternally(string url) => openUsingShellExecute(url);

        private void openUsingShellExecute(string path) => Process.Start(new ProcessStartInfo
        {
            FileName = path,
            UseShellExecute = true //see https://github.com/dotnet/corefx/issues/10361
        });

        public override ITextInputSource GetTextInput() => Window == null ? null : new GameWindowTextInput(Window);

        protected override IEnumerable<InputHandler> CreateAvailableInputHandlers()
        {
            var defaultEnabled = new InputHandler[]
            {
                new OsuTKMouseHandler(),
                new OsuTKKeyboardHandler(),
                new OsuTKJoystickHandler(),
            };

            var defaultDisabled = new InputHandler[]
            {
                new OsuTKRawMouseHandler(),
            };

            foreach (var h in defaultDisabled)
                h.Enabled.Value = false;

            return defaultEnabled.Concat(defaultDisabled);
        }

        public override Task SendMessageAsync(IpcMessage message) => ipcProvider.SendMessageAsync(message);

        protected override void Dispose(bool isDisposing)
        {
            ipcProvider?.Dispose();
            ipcThread?.Join(50);
            base.Dispose(isDisposing);
        }
    }
}
