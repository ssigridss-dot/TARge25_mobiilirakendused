namespace MyMauiApp;

public partial class ValgusfoorPage : ContentPage
{
    // Näitab, kas valgusfoor on sisse lülitatud
    private bool foorOn = false;

    public ValgusfoorPage()
    {
        InitializeComponent();

        // Lisame tuledele klõpsamise võimaluse
        LisaKlopsimine(redBox, "punane");
        LisaKlopsimine(yellowBox, "kollane");
        LisaKlopsimine(greenBox, "roheline");
    }

    // Sisse nupu vajutamine
    private void OnSisseClicked(object sender, EventArgs e)
    {
        foorOn = true;

        // Alguses kõik tuled välja
        redBox.Color = Colors.DarkRed;
        yellowBox.Color = Colors.DarkGoldenrod;
        greenBox.Color = Colors.DarkGreen;

        statusLabel.Text = "Vali valgus";
    }

    // Välja nupu vajutamine
    private void OnValjaClicked(object sender, EventArgs e)
    {
        foorOn = false;

        // Kõik tuled tagasi halliks
        redBox.Color = Colors.Gray;
        yellowBox.Color = Colors.Gray;
        greenBox.Color = Colors.Gray;

        // Foor on väljas
        statusLabel.Text = "Lülita esmalt foor sisse";
    }

    // Lisab tulele klõpsamise võimaluse
    private void LisaKlopsimine(BoxView box, string valgus)
    {
        var tapGesture = new TapGestureRecognizer();

        tapGesture.Tapped += async (sender, e) =>
        {
            // Kui foor on väljas, ei saa tulesid valida
            if (!foorOn)
            {
                statusLabel.Text = "Lülita esmalt foor sisse";
                return;
            }

            // Kõigepealt lülitame kõik tuled välja
            redBox.Color = Colors.DarkRed;
            yellowBox.Color = Colors.DarkGoldenrod;
            greenBox.Color = Colors.DarkGreen;

            // Valime klikitud tule
            if (valgus == "punane")
            {
                redBox.Color = Colors.Red;
                statusLabel.Text = "Seisa";
            }
            else if (valgus == "kollane")
            {
                yellowBox.Color = Colors.Yellow;
                statusLabel.Text = "Valmista";
            }
            else if (valgus == "roheline")
            {
                greenBox.Color = Colors.Green;
                statusLabel.Text = "Sõida";
            }

            // Väike animatsioon
            await Task.WhenAll(
                box.ScaleTo(1.2, 150),
                box.FadeTo(0.5, 150)
            );

            await Task.WhenAll(
                box.ScaleTo(1.0, 150),
                box.FadeTo(1.0, 150)
            );
        };

        box.GestureRecognizers.Add(tapGesture);
    }
}