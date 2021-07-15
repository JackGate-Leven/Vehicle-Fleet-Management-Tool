using System;

namespace MRRCManagement
{
    public class Navigation
    {
        private string currentMenu { set; get; } = "Home";
        private Program program = new Program();
        public void navigation(Program program)
        {
            // controls the navigation of the program and determines what menus are initiated
            Console.Clear();

            // determine which menu to go to from home
            switch (this.currentMenu)
            {
                // home menu
                case "Home":
                    {
                        HomeMenu();
                        break;
                    }

                // customer menu
                case "CustomerMenu":
                    {
                        CustomerMenu();
                        break;
                    }

                // fleet menu
                case "FleetMenu":
                    {
                        FleetMenu();
                        break;
                    }

                // Rental menu
                case "RentalMenu":
                    {
                        RentalMenu();
                        break;
                    }

                default:
                    {
                        return;
                    }

            }

            // Listening for user key press within navigation
            ConsoleKeyInfo keyPressed;
            do
            {
                keyPressed = Console.ReadKey();
                string keyPressedString = keyPressed.Key.ToString();

                // Checks for home or exit program input from customer
                switch (keyPressedString)
                {
                    case "Backspace":
                    case "H":
                        {
                            this.currentMenu = "Home";
                            Navigation();
                            return;
                        }

                    case "Q":
                    case "Escape":
                        {
                            SaveAllFiles();
                            return;
                        }
                }

                // Navigation checks followed by listening for user input
                switch (this.currentMenu)
                {
                    // if at Home menu
                    case "Home":
                        {
                            // Activities from Home menu
                            switch (keyPressedString)
                            {
                                case "A":
                                    {
                                        this.currentMenu = "CustomerMenu";
                                        Navigation();
                                        return;
                                    }

                                case "B":
                                    {
                                        this.currentMenu = "FleetMenu";
                                        Navigation();
                                        return;
                                    }

                                case "C":
                                    {
                                        this.currentMenu = "RentalMenu";
                                        Navigation();
                                        return;
                                    }

                                default:
                                    {
                                        Navigation();
                                        return;
                                    }
                            }
                        }

                    // If at Customer menu
                    case "CustomerMenu":
                        {
                            // Activities from Customer menu
                            switch (keyPressedString)
                            {
                                // if display all customers is selected
                                case "A":
                                    {
                                        CustomersDisplay();
                                        this.currentMenu = "CustomerMenu";
                                        Navigation();
                                        return;
                                    }

                                // if display select customer is selected
                                case "B":
                                    {
                                        CustomerDisplay();
                                        this.currentMenu = "CustomerMenu";
                                        Navigation();
                                        return;
                                    }

                                // if add customer is selected
                                case "C":
                                    {
                                        CustomerCreate();
                                        this.currentMenu = "CustomerMenu";
                                        Navigation();
                                        return;
                                    }

                                // if modify customer is selected
                                case "D":
                                    {
                                        CustomerModify();
                                        this.currentMenu = "CustomerMenu";
                                        Navigation();
                                        return;
                                    }

                                // if remove customer is selected
                                case "E":
                                    {
                                        CustomerRemove();
                                        this.currentMenu = "CustomerMenu";
                                        Navigation();
                                        return;
                                    }

                                default:
                                    {
                                        Navigation();
                                        return;
                                    }
                            }
                        }

                    // If at Fleet menu
                    case "FleetMenu":
                        {
                            // Activities from Customer menu
                            switch (keyPressedString)
                            {
                                // if display all vehicles is selected
                                case "A":
                                    {
                                        FleetDisplay();

                                        this.currentMenu = "FleetMenu";
                                        Navigation();
                                        return;
                                    }

                                // if create new vehicle is selected
                                case "B":
                                    {
                                        FleetCreate();

                                        this.currentMenu = "FleetMenu";
                                        Navigation();
                                        return;
                                    }

                                // if modify fleet vehicle is selected
                                case "C":
                                    {
                                        FleetModify();

                                        this.currentMenu = "FleetMenu";
                                        Navigation();
                                        return;
                                    }

                                // if remove fleet vehicle is selected
                                case "D":
                                    {
                                        FleetRemove();

                                        this.currentMenu = "FleetMenu";
                                        Navigation();
                                        return;
                                    }

                                default:
                                    {
                                        Navigation();
                                        return;
                                    }
                            }
                        }

                    // If at Rental menu
                    case "RentalMenu":
                        {
                            // Activities from Customer menu
                            switch (keyPressedString)
                            {
                                // if search for suitable rental is selected
                                case "A":
                                    {
                                        RentalSearch();
                                        this.currentMenu = "RentalMenu";
                                        Navigation();
                                        return;
                                    }


                                // if rent a vehicle is selected
                                case "B":
                                    {
                                        RentalRent();
                                        this.currentMenu = "RentalMenu";
                                        Navigation();
                                        return;
                                    }

                                // if return a vehicle is selected
                                case "C":
                                    {
                                        RentalReturn();
                                        this.currentMenu = "RentalMenu";
                                        Navigation();
                                        return;
                                    }


                                // if return report of rented vehicles is selected
                                case "D":
                                    {
                                        RentalReport();
                                        this.currentMenu = "RentalMenu";
                                        Navigation();
                                        return;
                                    }

                                default:
                                    {
                                        Navigation();
                                        return;
                                    }
                            }
                        }
                }
            } while (true);
        }
    }
    }

        
}
