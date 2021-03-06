﻿using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WorkshopMonitor.Overwatch;

namespace WorkshopMonitor.UI
{
    public class UIAssetTypeFilterOption : UIComponent
    {
        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        private UISprite _checkBox;
        private UISprite _icon;

        private AssetType _assetType;
        private string _sprite;
        private Color32? _iconColor;

        private bool _checked;

        public void Initialize(AssetType assetType)
        {
            _assetType = assetType;
            _sprite = UIConstants.GetAssetTypeSprite(assetType);
            _iconColor = UIConstants.GetAssetTypeColor(assetType);

            width = UIConstants.FilterCheckBoxSize + UIConstants.FilterCheckBoxIconDistance + UIConstants.FilterIconSize;
            height = UIConstants.FilterIconSize;
        }

        public override void Start()
        {
            base.Start();

            _checkBox = AddUIComponent<UISprite>();
            _checkBox.relativePosition = new Vector3(0, UIConstants.FilterCheckBoxYOffset);
            _checkBox.size = new Vector2(UIConstants.FilterCheckBoxSize, UIConstants.FilterCheckBoxSize);
            
            _icon = AddUIComponent<UISprite>();
            _icon.relativePosition = new Vector3(UIConstants.FilterCheckBoxIconDistance, 0);
            _icon.size = new Vector2(UIConstants.FilterIconSize, UIConstants.FilterIconSize);
            _icon.spriteName = _sprite;
            if (_iconColor.HasValue)
                _icon.color = _iconColor.Value;

            _checked = true;
            SetCheckboxSprite();

            eventClicked += UIFilterOption_eventClicked;
        }

        public void SetCheckedSilent(bool @checked)
        {
            _checked = @checked;
            SetCheckboxSprite();
        }

        public override void OnDestroy()
        {
            eventClicked -= UIFilterOption_eventClicked;
        }

        private void UIFilterOption_eventClicked(UIComponent component, UIMouseEventParameter eventParam)
        {
            _checked = !_checked;
            SetCheckboxSprite();
            OnCheckedChanged(_checked);
        }


        protected virtual void OnCheckedChanged(bool isChecked)
        {
            var handler = CheckedChanged;
            if (handler != null)
                handler.Invoke(this, new CheckedChangedEventArgs(_assetType, isChecked));
        }

        private void SetCheckboxSprite()
        {
            _checkBox.spriteName = _checked ? UIConstants.CheckboxCheckedSprite : UIConstants.CheckboxUnCheckedSprite;
        }
    }
}
