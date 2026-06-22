using System.Windows;
using auth_app.Models;
using auth_app.Services;

namespace auth_app.Views.Products;

public partial class ProductCreateWindow : Window
{
    private readonly ProductService _productService;

    public ProductCreateWindow(
        ProductService productService)
    {
        InitializeComponent();

        _productService = productService;
    }

    private async void Save_Click(
        object sender,
        RoutedEventArgs e)
    {
        var product = new Product
        {
            Name = NameBox.Text,
            Price = decimal.Parse(PriceBox.Text),
            Quantity = int.Parse(QuantityBox.Text),
            IsActive = true
        };

        await _productService.AddAsync(product);

        DialogResult = true;
    }
}