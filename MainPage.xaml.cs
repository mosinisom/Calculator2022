using Calculator2022.Logic;

namespace Calculator2022;

public partial class MainPage : ContentPage
{
	Calculator calc = new Calculator();

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnClick(object sender, EventArgs e)
	{
        calc.Press(((Button)sender).Text);
        scr.Text = calc.Screen;
	}
}

