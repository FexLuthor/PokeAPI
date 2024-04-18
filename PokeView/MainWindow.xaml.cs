using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PokeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int clickeditems;
        private string Pokemon = "";
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SystemMessage.Text = "";
            Pokemon = PokeInput.Text.ToLower();
            PokeModel PokemonDat = new PokeModel();
            PokemonDat = await Request.PokeCall(Pokemon);
            if (PokemonDat.OnlineSuccess)
            {

                PokeName.Content = Pokemon.ToUpper();

                for (int i = 0; i < PokemonDat.Type.Count; i++)
                {
                    PokeType.Text += PokemonDat.Type[i];
                    if (i < PokemonDat.Type.Count - 1)
                    {
                        PokeType.Text += ", ";
                    }
                }

                for (int i = 0; i < PokemonDat.Ability.Count; i++)
                {
                    PokeAbilities.Text += PokemonDat.Ability[i];

                    if (i < PokemonDat.Ability.Count - 1)
                    {
                        PokeAbilities.Text += ", ";
                    }
                }
                Image BildShiny = new Image();
                BildShiny.Source = new BitmapImage(new Uri(PokemonDat.ShinyUrl));
                PokeShiny.Source = BildShiny.Source;

                Image BildNormal = new Image();
                BildNormal.Source = new BitmapImage(new Uri(PokemonDat.PictureUrl));
                PokeNormal.Source = BildNormal.Source;

               




            }
            else
            {
                SystemMessage.Text = "Pokémon not found. Please check and reenter your Pokémons name.";
            }
            PokeInput.Clear();

        }
    }
}