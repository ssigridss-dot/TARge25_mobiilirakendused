namespace MyMauiApp;

public partial class TimerPage : ContentPage
{
    private bool _isRunning = false;

    public TimerPage()
    {
        InitializeComponent();
    }

    private async void ShowTime()
    {
        while (_isRunning)
        {
            // Kasutan MainThreadi, et tagada kasutajaliidese turvaline uuendamine
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (timer_btn != null)
                {
                    timer_btn.Text = DateTime.Now.ToString("T");
                }
            });

            await Task.Delay(1000);
        }
    }

    private void timer_btn_Clicked(object sender, EventArgs e)
    {
        if (!_isRunning)
        {
            _isRunning = true;
            ShowTime();
        }
        else
        {
            _isRunning = false; // Peatab taimeri uuel vajutusel
        }
    }

    private async void tagasi_Clicked(object sender, EventArgs e)
    {
        _isRunning = false; // Peatab tsükli lehelt lahkudes
        await Navigation.PopAsync();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        // Vajadusel saab siia lisada loogika
    }
}