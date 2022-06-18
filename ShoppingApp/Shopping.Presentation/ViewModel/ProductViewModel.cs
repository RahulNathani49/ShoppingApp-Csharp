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
       // public ObservableCollection<Products> CartItems
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

        public ProductViewModel()
        {

            Message = "Error Message";
            service = new Service();
            Products = CreateCollection();
            AddToCartCommand = new DelegateCommand(AddToCart);
            ViewCartCommand = new DelegateCommand(ViewCart);

        }

        public void ViewCart(object parameter) { }

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
        private void NotifyPropertyChanged(string parameterName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(parameterName));
        }
    }
  
}
