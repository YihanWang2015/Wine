//Author: Yihan Wang
//CIS 237
//Assignment 5
/*
 * The Menu Choices Displayed By The UI
 * 1. Load Wine List From CSV
 * 2. Print The Entire List Of Items
 * 3. Search For An Item
 * 4. Add New Item To The List
 * 5. Update an item
 * 6. Delete an Item from the list
 * 7. Exit Program
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class Program
    {
        static void Main(string[] args)
        {

            //Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            //Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            //Display the Menu and get the response. Store the response in the choice integer
            //This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            //import from the database         
            BeverageYWangEntities beverageYWangEntities = new BeverageYWangEntities();

            while (choice != 7)
            {
                switch (choice)
                {
                    case 1:
                        //Gain access to the database
                        Beverage beverage = new Beverage();

                       /*
                        if (beverage != null)
                        {
                            //Display Success Message
                            userInterface.DisplayImportSuccess();
                        }
                        else
                        {
                            //Display Fail Message
                            userInterface.DisplayImportError();
                        }
                       */
                        break;

                    case 2:
                        //Print Entire List Of Items

                        userInterface.DisplayAllItems(beverageYWangEntities);

                        break;

                    case 3:
                        //Search For An Item
                        string searchQuery = userInterface.GetSearchQuery();

                        Beverage foundBeverage = beverageYWangEntities.Beverages.Find(searchQuery);

                        if (foundBeverage != null)
                        {
                            userInterface.DisplayItemFound(foundBeverage);
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }
                        break;

                    case 4:
                        //Add A New Item To The List
                        string[] newItemInformation = userInterface.GetNewItemInformation();
                        if (beverageYWangEntities.Beverages.Find(newItemInformation[0]) == null)
                        {
                            Beverage newBeverage = new Beverage();

                            newBeverage.id = newItemInformation[0];
                            newBeverage.name = newItemInformation[1];
                            newBeverage.pack = newItemInformation[2];
                            newBeverage.price = Convert.ToDecimal(newItemInformation[3]);
                            newBeverage.active = Convert.ToBoolean(newItemInformation[4]);

                            //Add to the database
                            beverageYWangEntities.Beverages.Add(newBeverage);
                            //Save Changes to the database
                            beverageYWangEntities.SaveChanges();

                            //Successful add
                            userInterface.DisplayAddWineItemSuccess();

                        }
                        else
                        {
                            userInterface.DisplayItemAlreadyExistsError();
                        }

                        break;
                    case 5:
                        //Update Item to Database
                        string updateQuery = userInterface.GetSearchQuery();
                        //locate the item by Id
                        Beverage beverageForUpdate = beverageYWangEntities.Beverages.Find(updateQuery);

                        if (beverageForUpdate != null)
                        {
                            string[] updateItemInformation = userInterface.GetUpdateItemInformation();
                            
                                beverageForUpdate.name = updateItemInformation[0];
                                beverageForUpdate.pack = updateItemInformation[1];
                                beverageForUpdate.price = Convert.ToDecimal(updateItemInformation[2]);
                                beverageForUpdate.active = Convert.ToBoolean(updateItemInformation[3]);

                                //update to the database
                                beverageYWangEntities.Beverages.Add(beverageForUpdate);
                                //Save Changes to the database
                                beverageYWangEntities.SaveChanges();

                                //Successful update
                                userInterface.DisplayAddWineItemSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }

                        break;
                    case 6:
                        //Delete Item from Database
                        string deleteQuery = userInterface.GetSearchQuery();
                        Beverage beverageForDelete = beverageYWangEntities.Beverages.Find(deleteQuery);

                        if (beverageForDelete != null)
                        {
                            userInterface.DisplayItemFound(beverageForDelete);
                            //Delete the Record
                            beverageYWangEntities.Beverages.Remove(beverageForDelete);
                            //Save changes to database
                            beverageYWangEntities.SaveChanges();
                        }
                        else
                        {
                            userInterface.DisplayItemFoundError();
                        }

                        break;

                }

                //Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }

        }
    }
}
