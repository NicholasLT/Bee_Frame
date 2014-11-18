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

namespace BeeTest_Frame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _BeesGenerated;
        private BrushConverter bc = new BrushConverter();
        private ucBee lastAttacked;

        public bool BeesGenerated
        {
            get
            {
                return _BeesGenerated;
            }
            set
            {
                _BeesGenerated = value;
                if (_BeesGenerated == true)
                {
                    btnCreateBee.IsEnabled = false;
                    
                }
                else
                {
                    btnCreateBee.IsEnabled = true;
                }
            }
        }




        public MainWindow()
        {
            InitializeComponent();
        }


        private void btnCreateBee_Click(object sender, RoutedEventArgs e)
       {
           BeeCreation();

        }



        private void BeeCreation()
        {
            try
            {
                if (!BeesGenerated)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ucBee newUc = new ucBee(ucBee.BeeType.Drone, 100.0f);
                        stackBeesD.Children.Add(newUc);
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        ucBee newUc = new ucBee(ucBee.BeeType.Queen, 100.0f);
                        stackBeesQ.Children.Add(newUc);
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        ucBee newUc = new ucBee(ucBee.BeeType.Worker, 100.0f);
                        stackBeesW.Children.Add(newUc);
                    }

                    BeesGenerated = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Bee Creation Failed: {0}", ex.Message.ToString());
            }

        }

        private void btnAttackBee_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                    NonPreciseAttack();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Bee Attack Failed Failed: {0}", ex.Message.ToString());
            }


        }


        /// <summary>
        /// Non Precise Attack - Will continue to attack Dead Bee's
        /// </summary>
        private void NonPreciseAttack()
        {

            if (lastAttacked != null)
            {
                ResetColor(lastAttacked);
            }

            Random rnd = new Random();
            int containerId = rnd.Next(0, 3);
            StackPanel sp = (StackPanel)stackBees.Children[containerId];
            if (sp.Children.Count > 0)
            {
                //Select Random Bee based on the amount of bees
                int attackedBee = rnd.Next(0, sp.Children.Count);
                ucBee selectedBee = (ucBee)sp.Children[attackedBee];
                selectedBee.Damage(rnd.Next(0, 80));
                lastAttacked = selectedBee;
            }
        }

        private void ResetColor(ucBee inBee)
        {
            if (inBee != null)
            {
                lastAttacked.beeGrid.Background = (Brush)bc.ConvertFromString("#FF686868");
            }
        }


    }
}
