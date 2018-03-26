using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;


namespace Todos
{
    public sealed partial class NewPage : Page
    {
        public NewPage()
        {
            this.InitializeComponent();
        }

        private ViewModels.TodoItemViewModel ViewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ViewModel = (ViewModels.TodoItemViewModel)(e.Parameter);

            if (ViewModel.SelectedItem == null)
            {
                CreateButton.Content = "Create";
                var button = new MessageDialog("请创建Todo项目！").ShowAsync();
            }
            else
            {
                CreateButton.Content = "Update";
                var button = new MessageDialog("请修改Todo项目！").ShowAsync();

                title.Text = ViewModel.SelectedItem.title;
                description.Text = ViewModel.SelectedItem.description;
                DatePicker.Date = ViewModel.SelectedItem.time;
            }
        }

        private async void displayNoWords(int stateNum)
        {
            if (stateNum == 1)
            {
                ContentDialog warningDialog = new ContentDialog()
                {
                    Title = "Title和Details的内容为空",
                    Content = "请先输入Title和Details的内容",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await warningDialog.ShowAsync();
            }
            if (stateNum == 2)
            {
                ContentDialog warningDialog = new ContentDialog()
                {
                    Title = "Title的内容为空",
                    Content = "请先输入Title的内容",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await warningDialog.ShowAsync();
            }
            if (stateNum == 3)
            {
                ContentDialog warningDialog = new ContentDialog()
                {
                    Title = "Details的内容为空",
                    Content = "请先输入Details的内容",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await warningDialog.ShowAsync();
            }
            if (stateNum == 4)
            {
                ContentDialog warningDialog = new ContentDialog()
                {
                    Title = "Title和Details的内容为空,设定的日期不符合（需要大于等于今天）",
                    Content = "请先输入Title和Details的内容并且修改日期",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await warningDialog.ShowAsync();
            }
            if (stateNum == 5)
            {
                ContentDialog warningDialog = new ContentDialog()
                {
                    Title = "Title的内容为空，设定的日期不符合（需要大于等于今天）",
                    Content = "请先输入Title的内容并且修改日期",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await warningDialog.ShowAsync();
            }
            if (stateNum == 6)
            {
                ContentDialog warningDialog = new ContentDialog()
                {
                    Title = "Details的内容为空，设定的日期不符合（需要大于等于今天）",
                    Content = "请先输入Details的内容并且修改日期",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await warningDialog.ShowAsync();
            }
            if (stateNum == 7)
            {
                ContentDialog warningDialog = new ContentDialog()
                {
                    Title = "设定的日期不符合（需要大于等于今天）",
                    Content = "请修改日期",
                    PrimaryButtonText = "Ok"
                };
                ContentDialogResult result = await warningDialog.ShowAsync();
            }
        }

        private void DeleteButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.RemoveTodoItem(ViewModel.SelectedItem);
                Frame.Navigate(typeof(MainPage), ViewModel);
            }
        }
        private void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            //judge whether qualification satisfied
            bool TimeState = true;
            bool titleEmpty = false;
            bool descriptionEmpty = false;
            if (DatePicker.Date.AddDays(1) < DateTimeOffset.Now) TimeState = false;
            if (title.Text.Trim() == String.Empty) titleEmpty = true;
            if (description.Text.Trim() == String.Empty) descriptionEmpty = true;

            if (titleEmpty && descriptionEmpty && TimeState)
                displayNoWords(1);
            if (titleEmpty && !descriptionEmpty && TimeState)
                displayNoWords(2);
            if (!titleEmpty && descriptionEmpty && TimeState)
                displayNoWords(3);
            if (titleEmpty && descriptionEmpty && !TimeState)
                displayNoWords(4);
            if (titleEmpty && !descriptionEmpty && !TimeState)
                displayNoWords(5);
            if (!titleEmpty && descriptionEmpty && !TimeState)
                displayNoWords(6);
            if (!titleEmpty && !descriptionEmpty && !TimeState)
                displayNoWords(7);


            if (ViewModel.SelectedItem == null)
            {
                if (!titleEmpty && !descriptionEmpty && TimeState)
                {
                    string timeStr = DatePicker.Date.ToString();
                    ViewModel.AddTodoItem(title.Text, description.Text, timeStr);
                    Frame.Navigate(typeof(MainPage), ViewModel);
                }
            }
            else
            {
                if (!titleEmpty && !descriptionEmpty && TimeState)
                    UpdateButton_Clicked(sender, e);
            }
        }

        private void UpdateButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                ViewModel.UpdateTodoItem(DatePicker.Date.ToString(), title.Text, description.Text);
                Frame.Navigate(typeof(MainPage), ViewModel);
            }
        }

        private void CancelButton_Clicked(object sender, RoutedEventArgs e)
        {
            title.Text = String.Empty;
            description.Text = String.Empty;
            DatePicker.Date = DateTimeOffset.Now;
        }

    }
}
