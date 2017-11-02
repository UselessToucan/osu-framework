﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using osu.Framework.Extensions.TypeExtensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using OpenTK;
using OpenTK.Graphics;

namespace osu.Framework.Graphics.Visualisation
{
    internal class PropertyDisplay : VisibilityContainer
    {
        private readonly FillFlowContainer flow;

        private const float width = 600;

        protected override Container<Drawable> Content => flow;

        public PropertyDisplay()
        {
            Width = width;
            RelativeSizeAxes = Axes.Y;

            AddInternal(new ScrollContainer
            {
                Padding = new MarginPadding(10),
                RelativeSizeAxes = Axes.Both,
                ScrollbarOverlapsContent = false,
                Child = flow = new FillFlowContainer
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Direction = FillDirection.Vertical
                }
            });
        }

        public void UpdateFrom(Drawable source)
        {
            Clear();

            if (source == null)
                return;

            Type type = source.GetType();

            AddRange(((IEnumerable<MemberInfo>)type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.GetMethod != null)) // Get all properties that can have a value
                .Concat(type.GetFields(BindingFlags.Instance | BindingFlags.Public)) // And all fields
                .OrderBy(member => member.Name)
                .Concat(type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic).OrderBy(field => field.Name)) // Include non-public fields at the end
                .Select(member => new PropertyItem(member, source)));
        }

        protected override void PopIn()
        {
            this.ResizeWidthTo(width, 500, Easing.OutQuint);
        }

        protected override void PopOut()
        {
            this.ResizeWidthTo(0, 500, Easing.OutQuint);
        }

        private class PropertyItem : Container
        {
            private readonly SpriteText valueText;
            private readonly Box changeMarker;
            private readonly Func<object> getValue;

            public PropertyItem(MemberInfo info, IDrawable d)
            {
                Type type;
                switch (info.MemberType)
                {
                    case MemberTypes.Property:
                        PropertyInfo propertyInfo = (PropertyInfo)info;
                        type = propertyInfo.PropertyType;
                        getValue = () => propertyInfo.GetValue(d);
                        break;

                    case MemberTypes.Field:
                        FieldInfo fieldInfo = (FieldInfo)info;
                        type = fieldInfo.FieldType;
                        getValue = () => fieldInfo.GetValue(d);
                        break;

                    default:
                        throw new NotImplementedException(@"Not a value member.");
                }

                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;

                AddRangeInternal(new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Padding = new MarginPadding
                        {
                            Right = 6
                        },
                        Child = new FillFlowContainer<SpriteText>
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(10f),
                            Children = new[]
                            {
                                new SpriteText
                                {
                                    Text = info.Name,
                                    Colour = Color4.LightBlue,
                                },
                                new SpriteText
                                {
                                    Text = $@"[{type.Name}]:",
                                    Colour = Color4.MediumPurple,
                                },
                                valueText = new SpriteText
                                {
                                    Colour = Color4.White,
                                },
                            }
                        }
                    },
                    changeMarker = new Box
                    {
                        Size = new Vector2(4, 18),
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreRight,
                        Colour = Color4.Red
                    }
                });

                // Update the value once
                updateValue();
            }

            protected override void Update()
            {
                base.Update();
                updateValue();
            }

            private object lastValue;

            private void updateValue()
            {
                object value;
                try
                {
                    value = getValue() ?? "<null>";
                }
                catch (Exception e)
                {
                    value = $@"<{((e as TargetInvocationException)?.InnerException ?? e).GetType().ReadableName()} occured during evaluation>";
                }

                if (!value.Equals(lastValue))
                {
                    changeMarker.ClearTransforms();
                    changeMarker.Alpha = 0.8f;
                    changeMarker.FadeOut(200);
                }

                lastValue = value;
                valueText.Text = value.ToString();
            }
        }
    }
}
