using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ucBee.xaml
    /// </summary>
    /// 


    public partial class ucBee : UserControl, INotifyPropertyChanged, ICombatable
    {
        private delegate void DamageReceived(object sender, DamageEventArgs e);
        private event DamageReceived OnDamageReceived;
        private BrushConverter bc = new BrushConverter();

        //Public Properties for Databinding
        public float BeeHp;
        public bool AliveStatus;

        /// <summary>
        /// Type
        /// </summary>
        public enum BeeType
        {
            [Description("Worker")]
            Worker,

            [Description("Queen")]
            Queen,

            [Description("Drone")]
            Drone
        };


        /// <summary>
        /// Alive Status of Unit
        /// </summary>
        private bool _Alive;
        protected bool Alive 
        {
            get
            {
                return _Alive;
            }
            set
            {
                _Alive = value;
                AliveStatus = value;
                if (PropertyChanged != null)
                {
                    bee_PropertyChanged(this, new PropertyChangedEventArgs("Alive"));
                }
            }
        }



        /// <summary>
        /// Specifies the Type of Bee
        /// </summary>
        /// 
        private BeeType _BeeType;
        public BeeType TypeOfBee
        {
            get { return _BeeType; }
            set 
            {
                _BeeType = value;
                if (PropertyChanged != null)
                {
                    bee_PropertyChanged(this, new PropertyChangedEventArgs("TypeOfBee"));
                }
            }
        }


        /// <summary>
        /// Combat Health Pool - Linked up to Property Changed Event.
        /// Different To the Health Property used for working current health percentage.
        /// </summary>
        /// 
        private float _MaxHealth;
        protected float MaxHealth
        {
            get
            {
                return _MaxHealth;
            }
            set
            {
                _MaxHealth = value;
                if (PropertyChanged != null)
                {
                    bee_PropertyChanged(this, new PropertyChangedEventArgs("MaxHealth"));
                }
            }
        }

        /// <summary>
        /// Bee Health Property - Linked up to Property Changed Event.
        /// </summary>
        private float _Health;
        protected float Health
        {
            get
            {
                return _Health;
            }
            set
            {
                _Health = value;
                BeeHp = value;
                if (PropertyChanged != null)
                {
                    bee_PropertyChanged(this, new PropertyChangedEventArgs("Health"));
                }
            }
        }

        /// <summary>
        /// Initial Damage Method called - Fires the On damage received event for type-specific damage implementation.
        /// </summary>
        /// <param name="inDamage"></param>
        public void Damage(int inDamage)
        {
            if ((inDamage >=0) && (inDamage <=100))
            {
                DamageEventArgs e = new DamageEventArgs();
                e.DamageValue = inDamage;
                OnDamageReceived(this, e);

            }
        }


        /// <summary>
        /// Handles The damage taken by a Particular type of Bee.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bee_DamageReceived(object sender, DamageEventArgs e)
        {
            if (this.Alive)
                  this.Health -= e.DamageValue;
                  lblLastAttack.Content = " -" + e.DamageValue.ToString();
                  beeGrid.Background = (Brush)bc.ConvertFromString("#FFD45E5E");
                switch (this.TypeOfBee)
                {
                    case BeeType.Worker:
                            if (Health < (.7 * MaxHealth))
                            {
                                KillBee();
                            }
                        break;
                    case BeeType.Queen:
                            if (Health < (.2 * _MaxHealth))
                            {
                                KillBee();
                            }

                        break;
                    case BeeType.Drone:
                            if (Health < (.5 * MaxHealth))
                            {
                                KillBee();

                            }
                       break;
                    default:
                        break;
                }
       }

        /// <summary>
        /// Kills the bee.
        /// </summary>
        private void KillBee()
        {
            this.Alive = false;
            this.Health = 0.0f;
            lblLastAttack.Content = "☹";
        }

       
        /// <summary>
        /// Constructor - Sets Priliminary Variables - Based on the type of Bee Created and its Starting Health
        /// </summary>
        public ucBee(BeeType inType, float inHealth)
        {
            InitializeComponent();
            this.TypeOfBee = inType;
            this.Health = inHealth;
            this.MaxHealth = inHealth;
            this.OnDamageReceived += new DamageReceived(bee_DamageReceived);
            this.PropertyChanged += new PropertyChangedEventHandler(bee_PropertyChanged);
            this.Alive = true;
            DataContext = this;
        }


        /// <summary>
        /// Handles Any custom implementation of Bee variables 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bee_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                switch (e.PropertyName)
                {
                    case "Health":
                        txtHP.Text = Health.ToString();
                        progHP.Value = Health;
                        break;
                    case "Alive":
                        txtIsAlive.Text = Alive ? "Alive" : "Dead";
                        progHP.Value = Health;

                        //Death Check
                        if (!Alive)
                        {
                            this.IsEnabled = false;
                        }
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not update property " + e.PropertyName.ToString()  + "  : {0}", ex.Message.ToString());
                
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;




    }
}
