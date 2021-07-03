using AppView.ViewUsc;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AppView.View
{
    /// <summary>
    /// Lógica interna para HomeView.xaml
    /// </summary>
    public partial class HomeView : Window
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void HomeView_Loaded(object sender, RoutedEventArgs e)
        {
            //Iniciar com o menu aberto alnernar visibilidade dos botoes:
            ButtonCloseMenu.Visibility = Visibility.Visible;
            txtHome.Visibility = Visibility.Visible;//Texto/Logo FastDocs
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void HomeView_Closed(object sender, EventArgs e)
        {

        }

        //===========================================================================================================================================
        /// <summary>
        /// Efeito do menu lateral:
        /// Esconte somente os itens 
        /// A a animação é uma gatilho no XAML
        /// </summary>
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;

            ButtonOpenMenu.Visibility = Visibility.Collapsed;

            txtHome.Visibility = Visibility.Visible;//Texto/Logo FastDocs
        }

        //===========================================================================================================================================
        /// <summary>
        /// Efeito do menu lateral:
        /// Esconte somente os itens 
        /// A a animação é uma gatilho no XAML
        /// </summary>
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;

            ButtonOpenMenu.Visibility = Visibility.Visible;

            txtHome.Visibility = Visibility.Collapsed;

        }

        //===========================================================================================================================================
        /// <summary>
        /// Ações para cada opção lateral - lista de menus
        /// </summary>
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonCloseMenu_Click(sender, e);

            //Instacia um UserControl nulo:
            UserControl usc = null;

            //Limpa item da listView:
            GridMain.Children.Clear();

            // Pega o nome do list view clicado ListViewItem x:Name="ItemMenu01"
            string MnuClicado = ((ListViewItem)((ListView)sender).SelectedItem).Name;

            //Mostra o usc selecionado:
            switch (MnuClicado)
            {
                //mostrar user control confome menu clicado 
                case "MnuHome":
                    //Fechar Menu Lateral
                    ButtonCloseMenu_Click(sender, e);
                    //Limpa imtem da listView:
                    //GridMain.Children.Clear();//Inicio/Home

                    //MenuHome
                    usc = new usc_mnuHome();
                    GridMain.Children.Add(usc);

                    break;
                case "MnuVendas":
                    //Fechar Menu Lateral
                    ButtonCloseMenu_Click(sender, e);
                    MessageBox.Show("Menu Vendas em desenvolvimento!");
                    GridMain.Children.Clear();
                    break;
                case "MnuOrcamentos":
                    //Fechar Menu Lateral
                    ButtonCloseMenu_Click(sender, e);
                    usc = new usc_mnuOrcamentos();
                    GridMain.Children.Add(usc);
                    break;
                case "MnuOrdemServico":
                    //Fechar Menu Lateral
                    ButtonCloseMenu_Click(sender, e);
                    usc = new usc_msuOrdemServico();
                    GridMain.Children.Add(usc);
                    break;
                case "MnuCadastros":
                    //Fechar Menu Lateral
                    ButtonCloseMenu_Click(sender, e);
                    usc = new usc_mnuCadastrosGerais();
                    GridMain.Children.Add(usc);
                    break;
                case "MnuFinanceiro":
                    //Fechar Menu Lateral
                    ButtonCloseMenu_Click(sender, e);
                    usc = new usc_mnuFinanceiro();
                    GridMain.Children.Add(usc);
                    break;
                case "MnuIndicadores":
                    //Fechar Menu Lateral
                    ButtonCloseMenu_Click(sender, e);
                    usc = new usc_mnuIndicadores();// usc = new usc_Menu02()
                    GridMain.Children.Add(usc);
                    break;
                case "MnuConfigGerais":
                    usc = new usc_mnuConfigGerais();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
    }
}
