﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TableCloth.Components;

namespace TableCloth.ViewModels
{
    public class MainWindowV2ViewModel : INotifyPropertyChanged
    {
        public MainWindowV2ViewModel(
            SandboxLauncher sandboxLauncher,
            AppRestartManager appRestartManager)
        {
            _sandboxLauncher = sandboxLauncher;
            _appRestartManager = appRestartManager;
        }

#pragma warning disable IDE0051 // Remove unused private members
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = default)
#pragma warning restore IDE0051 // Remove unused private members
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName ?? string.Empty));

        private readonly SandboxLauncher _sandboxLauncher;
        private readonly AppRestartManager _appRestartManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public SandboxLauncher SandboxLauncher
            => _sandboxLauncher;

        public AppRestartManager AppRestartManager
            => _appRestartManager;

        public List<string> TemporaryDirectories { get; } = new();

        public string CurrentDirectory { get; set; }
    }
}
