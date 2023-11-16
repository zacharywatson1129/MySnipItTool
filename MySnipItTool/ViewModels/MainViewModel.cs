using DrawingShapesLibrary;
using MySnipItTool.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySnipItTool.ViewModels
{
    public class MainViewModel
    {
        private DrawingMode toolSelected;

        // public ObservableCollection<IShape> Shapes { get; set; }
        public ObservableCollection<TabItem> Tabs { get; set; }
        public MainViewModel()
        {
            Tabs = new ObservableCollection<TabItem>();
            Tabs.Add(new TabItem { Header = "One"});
            Tabs.Add(new TabItem { Header = "Two"});

            // Shapes = new ObservableCollection<IShape>();
            
        }
    }

    public sealed class TabItem
    {
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
