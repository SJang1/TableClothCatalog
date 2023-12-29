﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TableCloth;

internal static class Extensions
{
    public static HttpClient CreateTableClothHttpClient(this IHttpClientFactory httpClientFactory)
    {
        if (httpClientFactory == null)
            throw new ArgumentNullException(nameof(httpClientFactory));

        return httpClientFactory.CreateClient(nameof(TableCloth));
    }

    public static void AddCommands(this IServiceCollection services, params Type[] commandTypes)
    {
        foreach (var eachCommandType in commandTypes)
            services.AddSingleton(eachCommandType);
    }

    public static IServiceCollection AddWindow<TWindow, TViewModel>(this IServiceCollection services,
        Func<IServiceProvider, TWindow>? windowImplementationFactory = default,
        Func<IServiceProvider, TViewModel>? viewModelImplementationFactory = default)
        where TWindow : Window
        where TViewModel : class
    {
        if (windowImplementationFactory != null)
            services.AddTransient<TWindow>(windowImplementationFactory);
        else
            services.AddTransient<TWindow>();

        if (viewModelImplementationFactory != null)
            services.AddTransient<TViewModel>(viewModelImplementationFactory);
        else
            services.AddTransient<TViewModel>();

        return services;
    }

    public static IServiceCollection AddPage<TPage, TViewModel>(this IServiceCollection services,
        bool addPageAsSingleton = false,
        Func<IServiceProvider, TPage>? pageImplementationFactory = default,
        Func<IServiceProvider, TViewModel>? viewModelImplementationFactory = default)
        where TPage : Page
        where TViewModel : class
    {
        if (addPageAsSingleton)
        {
            if (pageImplementationFactory != null)
                services.AddSingleton<TPage>(pageImplementationFactory);
            else
                services.AddSingleton<TPage>();
        }
        else
        {
            if (pageImplementationFactory != null)
                services.AddTransient<TPage>(pageImplementationFactory);
            else
                services.AddTransient<TPage>();
        }

        if (viewModelImplementationFactory != null)
            services.AddTransient<TViewModel>(viewModelImplementationFactory);
        else
            services.AddTransient<TViewModel>();

        return services;
    }

    // https://stackoverflow.com/a/2914599
    public static bool TryLeaveFocus(this FrameworkElement? targetElement)
    {
        if (targetElement == null)
            return false;

        var parent = (FrameworkElement)targetElement.Parent;
        while (parent != null && parent is IInputElement && !((IInputElement)parent).Focusable)
            parent = (FrameworkElement)parent.Parent;

        var scope = FocusManager.GetFocusScope(targetElement);
        FocusManager.SetFocusedElement(scope, parent as IInputElement);
        return true;
    }
}
