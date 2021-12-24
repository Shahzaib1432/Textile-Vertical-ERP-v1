﻿using GlobalLib.Others.ExtensionMethods;
using ManageNazyOrders.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManageNazyOrders.Controls
{
    /// <summary>
    /// Interaction logic for PurchaseRow.xaml
    /// </summary>
    public partial class PurchaseRow : UserControl
    {
        readonly TotalChangedDelegate TotalChanged;

        public PurchaseRow(TotalChangedDelegate totalChanged, string input = null)
        {
            InitializeComponent();
            AssignEvents();
            this.TotalChanged = totalChanged;
            CompiledString = input;
        }

        public int CurrentTotal { get; set; }

        private void AssignEvents()
        {
            void CalculateTotal()
            {
                var rate = RateBx.Text.TryToInt();
                var qty = QtyBx.Text.TryToInt();
                var total = rate * qty;
                TotalBlk.Text = total.ToString();
                CurrentTotal = total;
                if (TotalChanged != null)
                    TotalChanged();
            }

            RateBx.TextChanged += (a, b) => CalculateTotal();
            QtyBx.TextChanged += (a, b) => CalculateTotal();
        }

        public string CompiledString
        {
            get => GetString();
            set => SetString(value);
        }

        private void SetString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return;
            
            var splits = str.Split(',');
            ColorBx.Text = splits[1];
            CategoryBx.Text = splits[2];
            SubCategoryBx.Text = splits[3];
            DescriptionBx.Text = splits[4];
            UnitBx.Text = splits[5];
            RateBx.Text = splits[6];
            QtyBx.Text = splits[7];
        }

        private string GetString()
        {
            string output = "";
            output += MaxID.ToString() + ",";
            output += ColorBx.Text + ",";
            output += CategoryBx.Text + ",";
            output += SubCategoryBx.Text + ",";
            output += DescriptionBx.Text + ",";
            output += UnitBx.Text + ",";
            output += RateBx.Text + ",";
            output += QtyBx.Text;
            return output;
        }

        private int MaxID
        {
            get
            {
                var value = AddWorkOrder.UsedIDs.Max(i => i) + 1;
                AddWorkOrder.UsedIDs.Add(value);
                return value;
            }
        }

        public delegate void TotalChangedDelegate();
    }
}
