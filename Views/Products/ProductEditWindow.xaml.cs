using System.Windows;
using auth_app.Models;
using auth_app.Services;

namespace auth_app.Views.Products;

public partial class ProductEditWindow : Window
{
    private readonly ProductService _productService;

    private readonly Product _product;

    public ProductEditWindow(
        ProductService productService,
        Product product)
    {
        InitializeComponent();

        _productService = productService;
        _product = product;

        NameBox.Text = product.Name;
        PriceBox.Text = product.Price.ToString();
        QuantityBox.Text = product.Quantity.ToString();
    }

    private async void Save_Click(
        object sender,
        RoutedEventArgs e)
    {
        _product.Name = NameBox.Text;
        _product.Price = decimal.Parse(PriceBox.Text);
        _product.Quantity = int.Parse(QuantityBox.Text);

        await _productService.UpdateAsync(_product);

        DialogResult = true;
    }
}