namespace MauiApp;

public partial class TreePage : ContentPage
{
    private Random random = new Random();

    private bool animationRunning = false;

    public TreePage()
    {
        InitializeComponent();

        SpeedStepper.ValueChanged += OnSpeedChanged;
    }

    // NUPP "KÄIVITA"

    private async void OnActionClicked(object sender, EventArgs e)
    {
        if (animationRunning)
        {
            StatusLabel.Text = "Animatsioon juba töötab!";
            return;
        }

        if (ActionPicker.SelectedIndex == -1)
        {
            StatusLabel.Text = "Palun vali tegevus!";
            return;
        }

        string action = ActionPicker.SelectedItem.ToString();

        switch (action)
        {
            case "Kasva":
                await GrowTree();
                break;

            case "Õitse":
                await BloomTree();
                break;

            case "Värise":
                await ShakeTree();
                break;

            case "Langeta":
                await CutTree();
                break;
        }
    }

    // ANIMATSIOONI KIIRUS

    private void OnSpeedChanged(object sender, ValueChangedEventArgs e)
    {
        SpeedLabel.Text = $"{e.NewValue:0} ms";
    }

    // SLIDER - LEHESTIKU LÄBIPAISTVUS

    private void OnOpacityChanged(object sender, ValueChangedEventArgs e)
    {
        Leaves1.Opacity = e.NewValue;
        Leaves2.Opacity = e.NewValue;
        Leaves3.Opacity = e.NewValue;
    }

    // KASVA

    private async Task GrowTree()
    {
        animationRunning = true;

        StatusLabel.Text = "Puu kasvab!";

        uint speed = (uint)SpeedStepper.Value;

        // Väike juhuslik kasv.
        double growth = random.NextDouble() * 0.5 + 1.2;

        await Task.WhenAll(
            TreeTrunk.ScaleTo(growth, speed),
            Leaves1.ScaleTo(growth, speed),
            Leaves2.ScaleTo(growth, speed),
            Leaves3.ScaleTo(growth, speed)
        );

        StatusLabel.Text = "Puu on suurem!";

        animationRunning = false;
    }

    // ÕITSEMINE

    private async Task BloomTree()
    {
        animationRunning = true;

        StatusLabel.Text = "Puu õitseb!";

        uint speed = (uint)SpeedStepper.Value;

        // Näitame õisi.
        Flower1.IsVisible = true;
        Flower2.IsVisible = true;
        Flower3.IsVisible = true;

        // Alguses on õied väikesed.
        Flower1.Scale = 0;
        Flower2.Scale = 0;
        Flower3.Scale = 0;

        await Task.WhenAll(
            Flower1.ScaleTo(1, speed),
            Flower2.ScaleTo(1, speed),
            Flower3.ScaleTo(1, speed)
        );

        StatusLabel.Text = "Puu õitseb!";

        animationRunning = false;
    }

    // VÄRISE TUULES

    private async Task ShakeTree()
    {
        animationRunning = true;

        StatusLabel.Text = "Puu väriseb tuules!";

        uint speed = (uint)Math.Max(100, SpeedStepper.Value / 4);

        for (int i = 0; i < 4; i++)
        {
            await TreeArea.TranslateTo(15, 0, speed);

            await TreeArea.TranslateTo(-15, 0, speed);

            await TreeArea.TranslateTo(10, 0, speed);

            await TreeArea.TranslateTo(-10, 0, speed);

            await TreeArea.TranslateTo(0, 0, speed);
        }

        StatusLabel.Text = "Tuul vaibus.";

        animationRunning = false;
    }

    // LANGETA

    private async Task CutTree()
    {
        // Tingimuste kontroll.
        int month = SeasonPicker.Date.Month;
        int hour = TimeSelector.Time?.Hours ?? 0;

        bool isWinter =
            month == 12 ||
            month == 1 ||
            month == 2;

        bool isDay =
            hour >= 8 &&
            hour <= 17;

        if (!isWinter || !isDay)
        {
            StatusLabel.Text =
                "Puid võib langetada ainult talvel kell 08:00–17:00.";

            return;
        }

        animationRunning = true;

        StatusLabel.Text = "Puu langeb!";

        uint speed = (uint)SpeedStepper.Value;

        // Puu kukub külili.
        await TreeArea.RotateTo(90, speed);

        await TreeArea.FadeTo(0, speed);

        StatusLabel.Text = "Puu on langetatud.";

        animationRunning = false;
    }
}