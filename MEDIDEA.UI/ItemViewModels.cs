using DevExpress.Mvvm;
using System;
using System.Collections.Generic;

namespace MEDIDEA.UI
{
    public interface IHamburgerMenuItemViewModel { }
    public interface IHamburgerMenuBottomBarItemViewModel { }
    public abstract class ItemModelBase : BindableBase
    {
        public bool IsAlternatePlacementItem
        {
            get { return GetProperty(() => IsAlternatePlacementItem); }
            set { SetProperty(() => IsAlternatePlacementItem, value); }
        }
        public bool CloseMenuOnClick
        {
            get { return GetProperty(() => CloseMenuOnClick); }
            set { SetProperty(() => CloseMenuOnClick, value); }
        }
    }
    public abstract class GlyphItemModel : ItemModelBase
    {
        public string Glyph
        {
            get { return GetProperty(() => Glyph); }
            set { SetProperty(() => Glyph, value); }
        }
    }
    public abstract class ContentItemModelWith : GlyphItemModel
    {
        public object Content
        {
            get { return GetProperty(() => Content); }
            set { SetProperty(() => Content, value); }
        }
        public ContentItemModelWith(object content)
        {
            Content = content;
        }
    }
    public abstract class SelectableItemModel : ContentItemModelWith
    {
        public bool IsSelected
        {
            get { return GetProperty(() => IsSelected); }
            set { SetProperty(() => IsSelected, value); }
        }
        public bool SelectOnClick
        {
            get { return GetProperty(() => SelectOnClick); }
            set { SetProperty(() => SelectOnClick, value); }
        }
        public SelectableItemModel(object content) : base(content)
        {
            SelectOnClick = true;
        }
    }
    public class SeparatorItem : ItemModelBase, IHamburgerMenuItemViewModel
    {
    }
    public class HyperlinkItemModel : ItemModelBase, IHamburgerMenuItemViewModel
    {
        public Uri Link
        {
            get { return GetProperty(() => Link); }
            set { SetProperty(() => Link, value); }
        }
        public string LinkHeader
        {
            get { return GetProperty(() => LinkHeader); }
            set { SetProperty(() => LinkHeader, value); }
        }

        public HyperlinkItemModel(string header, Uri link)
        {
            LinkHeader = header;
            Link = link;
        }
    }
    public class NavigationItemModel : SelectableItemModel, IHamburgerMenuItemViewModel
    {
        public string NavigationTargetName
        {
            get { return GetProperty(() => NavigationTargetName); }
            set { SetProperty(() => NavigationTargetName, value); }
        }
        public Type NavigationTarget
        {
            get { return GetProperty(() => NavigationTarget); }
            set { SetProperty(() => NavigationTarget, value); }
        }
        public object NavigationParameter
        {
            get { return GetProperty(() => NavigationParameter); }
            set { SetProperty(() => NavigationParameter, value); }
        }
        public object RightContent
        {
            get { return GetProperty(() => RightContent); }
            set { SetProperty(() => RightContent, value); }
        }

        public NavigationItemModel(object content) : base(content) { }
    }
    public class SubMenuNavigationItemModel : NavigationItemModel, IHamburgerMenuItemViewModel
    {
        public object PreviewContent
        {
            get { return GetProperty(() => PreviewContent); }
            set { SetProperty(() => PreviewContent, value); }
        }
        public bool ShowInPreview
        {
            get { return GetProperty(() => ShowInPreview); }
            set { SetProperty(() => ShowInPreview, value); }
        }
        public bool ShowMark
        {
            get { return GetProperty(() => ShowMark); }
            set { SetProperty(() => ShowMark, value); }
        }

        public SubMenuNavigationItemModel(object content, object previewContent) : base(content)
        {
            PreviewContent = previewContent;
        }
    }
    public class SubMenuItemModel : ContentItemModelWith, IHamburgerMenuItemViewModel
    {
        public bool IsStandaloneSelection
        {
            get { return GetProperty(() => IsStandaloneSelection); }
            set { SetProperty(() => IsStandaloneSelection, value); }
        }

        public List<SubMenuNavigationItemModel> Items { get; private set; }

        public SubMenuItemModel(object content) : base(content)
        {
            Items = new List<SubMenuNavigationItemModel>();
        }
    }
    public class BottomBarNavigationItemModel : GlyphItemModel, IHamburgerMenuBottomBarItemViewModel
    {
        public string NavigationTargetName
        {
            get { return GetProperty(() => NavigationTargetName); }
            set { SetProperty(() => NavigationTargetName, value); }
        }
        public Type NavigationTarget
        {
            get { return GetProperty(() => NavigationTarget); }
            set { SetProperty(() => NavigationTarget, value); }
        }
        public object NavigationParameter
        {
            get { return GetProperty(() => NavigationParameter); }
            set { SetProperty(() => NavigationParameter, value); }
        }
    }
    public class BottomBarCheckableItemModel : GlyphItemModel, IHamburgerMenuBottomBarItemViewModel
    {
        public bool IsChecked
        {
            get { return GetProperty(() => IsChecked); }
            set { SetProperty(() => IsChecked, value); }
        }
    }
    public class BottomBarRadioItemModel : BottomBarCheckableItemModel, IHamburgerMenuBottomBarItemViewModel
    {
        public string GroupName
        {
            get { return GetProperty(() => GroupName); }
            set { SetProperty(() => GroupName, value); }
        }
        public BottomBarRadioItemModel(string groupName)
        {
            GroupName = groupName;
        }
    }
}