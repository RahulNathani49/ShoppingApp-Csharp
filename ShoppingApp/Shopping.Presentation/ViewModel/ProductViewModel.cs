using Shopping.Core.Entities;
using Shopping.Data;
using Shopping.Domain.Services;
using Shopping.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Presentation.ViewModel
{
    public class ProductViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<Products> Products { get; }

        public ObservableCollection<Cart> listOfCartItems;
        
        public ObservableCollection<Cart> ListOfCartItems {
            get { return listOfCartItems; } 
            set
            {
                listOfCartItems = value;
                NotifyPropertyChanged(nameof(ListOfCartItems));
            }
        }
        public DelegateCommand AddToCartCommand { get; }
        public DelegateCommand ViewCartCommand { get; }
        private readonly Service service;

        public Products SelectedItem { get; set; }

        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { 
                quantity = value;
                AddToCartCommand.NotifyCanExecuteChanged();

            }
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                NotifyPropertyChanged(nameof(Message));

            }
        }

        private string totalCartPrice;
        public string TotalCartPrice
        {
            get
            {
                return totalCartPrice;
            }
            set
            {
                totalCartPrice = value;
                NotifyPropertyChanged(nameof(TotalCartPrice));

            }
        }

        public ProductViewModel()
        {

            Message = "Error Message";
            service = new Service();
            Products = CreateCollection();
            ListOfCartItems = CreateCartCollection(1001);

            AddToCartCommand = new DelegateCommand(AddToCart);
                ViewCartCommand = new DelegateCommand(ViewCart);

            TotalCartPrice = "Total Price=";
        }

        public void ViewCart(object parameter) {
            int userID;
            userID = service.getUserId("Het");
            ListOfCartItems= CreateCartCollection(userID);
            decimal finalPrice = getFinalPrice(ListOfCartItems);
            TotalCartPrice = "Total Price="+finalPrice.ToString();
        }

        private decimal getFinalPrice(ObservableCollection<Cart> listOfCartItems)
        {
            decimal total = 0;
           for(int i= 0; i < listOfCartItems.Count; i++)
            {
                total += listOfCartItems[i].ItemTotal;
            }
            return total;
        }

        public void AddToCart(object parameter)
        {
            int userID;
            if (SelectedItem != null)
            {
                //userID=service.getUserId(UserViewModel.loggedUser);
                userID = service.getUserId("Het");
                bool isAddedToCart=   service.addToCart(userID,SelectedItem.ProductId ,Quantity);
               
                Console.WriteLine(isAddedToCart);
                Quantity = 0;
                Message = "Succesfull Added to Cart !!!";


            }
            else
            {
                Message = "Add to Cart Failed XXX";
            }
           
        }

        public ObservableCollection<Products> CreateCollection()
        {
            List<Products> products = service.GetProducts();
            
            return new ObservableCollection<Products>(products);
        }
        public ObservableCollection<Cart> CreateCartCollection(int userID)
        {
            List<Cart> cart = service.getCartItems(userID);

            return new ObservableCollection<Cart>(cart);
        }
        private void NotifyPropertyChanged(string parameterName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(parameterName));
        }
    }
  
}
