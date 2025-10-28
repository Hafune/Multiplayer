using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Lib
{
    public enum TooltipAlign
    {
        Above,
        Below,
        Left,
        Right
    }

    public static class MyVisualElementExtensions
    {
        public class NavigationContextEvent : NavigationEventBase<NavigationContextEvent>
        {
        }

        public class NavigationResetEvent : NavigationEventBase<NavigationResetEvent>
        {
        }

        public static void SendNavigationContextEvent(this VisualElement ele)
        {
            using var e = EventBase<NavigationContextEvent>.GetPooled();
            ele.SendEvent(e);
        }

        public static void SendNavigationResetEvent(this VisualElement ele)
        {
            using var e = EventBase<NavigationResetEvent>.GetPooled();
            ele.SendEvent(e);
        }

        public static void WarpCursorPosition(this VisualElement ele, Vector2? offset = null)
        {
#if !UNITY_WEBGL
            offset ??= Vector2.zero;

            var bounds = ele.worldBound;
            var _offset = bounds.size / 2f * offset.Value;
            var pos = bounds.center + _offset;
            pos.y = Screen.height - pos.y;
            Mouse.current?.WarpCursorPosition(pos);
#endif
        }

        public static bool IsDisplayFlex(this VisualElement ele) =>
            ele.style.display.keyword != StyleKeyword.Null && ele.style.display.value == DisplayStyle.Flex;

        public static void SetDisplay(this VisualElement ele, bool enable)
        {
            if (enable)
                ele.DisplayFlex();
            else
                ele.DisplayNone();
        }

        public static void DisplayFlex(this VisualElement ele) =>
            ele.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

        public static void DisplayNone(this VisualElement ele)
        {
#if UNITY_EDITOR
            if (ele == null)
                return;
#endif
            ele.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        public static bool FocusFirstFocusableElement(this VisualElement ele)
        {
            var firstEle = FindFirstFocusable(ele);

            if (firstEle is null)
                return false;

            firstEle.Focus();
            return true;
        }

        public static bool IsFocused(this VisualElement ele) =>
            ele.panel.focusController.focusedElement == ele;

        [CanBeNull]
        private static VisualElement FindFirstFocusable(VisualElement parent)
        {
            return parent
                .Children()
                .Where(ele => ele.visible)
                .Select(ele => ele.focusable ? ele : FindFirstFocusable(ele))
                .FirstOrDefault(ele => ele is not null);
        }

        private static bool IsVisibleInHierarchy(VisualElement ele)
        {
            if (!ele.visible)
                return false;

            return ele.parent is null || IsVisibleInHierarchy(ele.parent);
        }

        private static bool InDisplayInHierarchy(VisualElement ele) =>
            IsDisplayFlex(ele) && (ele.parent is null || InDisplayInHierarchy(ele.parent));

        public static Sprite GetBackgroundImage(this VisualElement ele) =>
            ele.style.backgroundImage.value.sprite;

        public static void SetBackgroundImage(this VisualElement ele, Sprite backgroundImage) =>
            ele.style.backgroundImage = new StyleBackground(backgroundImage);

        public static void SetBackgroundColor(this VisualElement ele, Color color) =>
            ele.style.backgroundColor = new StyleColor(color);

        public static void StyleTranslateByWorldPosition(
            this VisualElement ele,
            Camera camera,
            Vector3 pos,
            SpriteAlignment align = SpriteAlignment.Center)
        {
            var position = RuntimePanelUtils.CameraTransformWorldToPanel(ele.panel, pos, camera);
            var style = ele.resolvedStyle;
            var width = style.width;
            var height = style.height;

            var offset = align switch
            {
                SpriteAlignment.Center => new Vector2(-width / 2, -height / 2),
                SpriteAlignment.TopLeft => new Vector2(-width, -height),
                SpriteAlignment.TopCenter => new Vector2(-width / 2, -height),
                SpriteAlignment.TopRight => new Vector2(0, -height),
                SpriteAlignment.LeftCenter => new Vector2(-width, -height / 2),
                SpriteAlignment.RightCenter => new Vector2(0, -height / 2),
                SpriteAlignment.BottomLeft => new Vector2(-width, 0),
                SpriteAlignment.BottomCenter => new Vector2(-width / 2, 0),
                SpriteAlignment.BottomRight => Vector2.zero,
                SpriteAlignment.Custom => Vector2.zero,
                _ => throw new ArgumentOutOfRangeException(nameof(align), align, null)
            };

            var x = position.x + offset.x;
            var y = position.y + offset.y;

            var parentStyle = ele.parent.resolvedStyle;
            x = Math.Max(0, x);
            y = Math.Max(0, y);
            x = Math.Min(parentStyle.width - style.width, x);
            y = Math.Min(parentStyle.height - style.height, y);

            StyleTranslate styleTranslate = new();
            styleTranslate.value = new(x, y);
            ele.style.translate = styleTranslate;
        }

        public static Vector3 CalculateWorldPosition(
            this VisualElement ele,
            Camera camera,
            float zDistance,
            float offsetX = .5f,
            float offsetY = .5f
        )
        {
            var screenPoint = ele.worldBound.min + ele.worldBound.size * new Vector2(offsetX, offsetY);
            screenPoint.y = ele.panel.visualTree.worldBound.height - screenPoint.y;
            
            //не проверено
            //var scaleFactor = ele.panel.scaledPixelsPerPoint;
            //screenPoint *= scaleFactor;
            
            return camera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, zDistance));
        }

        public static Ray CalculateRay(
            this VisualElement ele,
            Camera camera,
            float offsetX = .5f,
            float offsetY = .5f
        )
        {
            var screenPoint = ele.worldBound.min + ele.worldBound.size * new Vector2(offsetX, offsetY);
            screenPoint.y = ele.panel.visualTree.worldBound.height - screenPoint.y;
    
            var panel = ele.panel;
            var scaleFactor = panel.scaledPixelsPerPoint;
    
            screenPoint *= scaleFactor;
    
            return camera.ScreenPointToRay(new Vector3(screenPoint.x, screenPoint.y, 0));
        }

        public static void StyleTranslate(this VisualElement ele, Vector2 position) =>
            ele.style.translate = new() { value = new(position.x, position.y) };

        public static T SimpleCopy<T>(this T original) where T : VisualElement, new()
        {
            var copy = Activator.CreateInstance(original.GetType()) as T;

            copy!.name = original.name;
            copy.visible = original.visible;
            copy.focusable = original.focusable;
            copy.pickingMode = original.pickingMode;
            copy.tabIndex = original.tabIndex;
            copy.usageHints = original.usageHints;
            copy.tooltip = original.tooltip;

            // copy.SetDisplay(original.IsDisplayFlex());

            if (original.GetClasses() is List<string> classList)
            {
                int c = classList.Count;
                for (int i = 0; i < c; i++)
                    copy.AddToClassList(classList[i]);
            }

            foreach (var child in original.Children())
                copy.Add(SimpleCopy(child));

            return copy;
        }

        public static void RegisterCallbacks<T0, T1>(this VisualElement ele, Action callback)
            where T0 : EventBase<T0>, new()
            where T1 : EventBase<T1>, new()
        {
            ele.RegisterCallback<T0>(_ => callback());
            ele.RegisterCallback<T1>(_ => callback());
        }

        public static void RegisterPointerEnterLeaveEvents(this VisualElement ele, Action onEnter, Action onLeave)
        {
            ele.RegisterCallback<PointerEnterEvent>(_ => onEnter());
            ele.RegisterCallback<PointerLeaveEvent>(_ => onLeave());
        }

        public static void SetPosition(this VisualElement element, VisualElement targetElement, TooltipAlign align) =>
            PositionElement(element, targetElement, align);

        private static void PositionElement(VisualElement element, VisualElement targetElement, TooltipAlign align)
        {
            var window = targetElement.panel.visualTree.worldBound;
            var elementBounds = element.worldBound;
            var targetBounds = targetElement.worldBound;

            var position = targetBounds.position;

            switch (align)
            {
                case TooltipAlign.Above:
                    position.x += (targetBounds.width - elementBounds.width) / 2;
                    position.y -= elementBounds.height;
                    break;

                case TooltipAlign.Below:
                    position.x += (targetBounds.width - elementBounds.width) / 2;
                    position.y += targetBounds.height;
                    break;

                case TooltipAlign.Left:
                    position.x -= elementBounds.width;
                    position.y += (targetBounds.height - elementBounds.height) / 2;
                    break;

                case TooltipAlign.Right:
                    position.x += targetBounds.width;
                    position.y += (targetBounds.height - elementBounds.height) / 2;
                    break;
            }

            // Ограничиваем позицию в пределах окна
            position.x = Mathf.Clamp(position.x, 0, window.width - elementBounds.width);
            position.y = Mathf.Clamp(position.y, 0, window.height - elementBounds.height);

            element.transform.position = position;
        }
    }
}