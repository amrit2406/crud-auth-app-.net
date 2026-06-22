using System.Windows;
using auth_app.Models;
using auth_app.Services;
using Microsoft.Extensions.DependencyInjection;

namespace auth_app.Views.Products;

public partial class ProductManagementWindow : Window
{
    private readonly ProductService _productService;

    public ProductManagementWindow(
        ProductService productService)
    {
        InitializeComponent();

        _productService = productService;

        Loaded += ProductManagementWindow_Loaded;
    }

    private async void ProductManagementWindow_Loaded(
        object sender,
        RoutedEventArgs e)
    {
        ProductsGrid.ItemsSource =
            await _productService.GetAllAsync();
    }

    private async void AddButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        var window =
            App.ServiceProvider
                .GetRequiredService<ProductCreateWindow>();

        if (window.ShowDialog() == true)
        {
            ProductsGrid.ItemsSource =
                await _productService.GetAllAsync();
        }
    }

    private async void EditButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (((FrameworkElement)sender).DataContext
            is not Product product)
            return;

        var window =
            new ProductEditWindow(
                _productService,
                product);

        if (window.ShowDialog() == true)
        {
            ProductsGrid.ItemsSource =
                await _productService.GetAllAsync();
        }
    }

    private async void DeleteButton_Click(
        object sender,
        RoutedEventArgs e)
    {
        if (((FrameworkElement)sender).DataContext
            is not Product product)
            return;

        await _productService.DeleteAsync(product.Id);

        ProductsGrid.ItemsSource =
            await _productService.GetAllAsync();
    }
}