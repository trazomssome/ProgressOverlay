using ProgressViewSample.Infrastructure;
using ProgressViewSample.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgressViewSample.ViewModels
{
    public class ProgressSupportingViewModel : ViewModelBase, IProgressSupportingViewModel
    {
        private bool _isProgressVisible = false;
        private double? _progressValue = null;
        private string _progressMessage = string.Empty;
        private ProgressStyle _progressStyle = ProgressStyle.Indeterminate;

        public bool IsProgressVisible
        {
            get => _isProgressVisible;
            private set { _isProgressVisible = value; OnPropertyChanged(); }
        }

        public double? ProgressValue
        {
            get => _progressValue;
            private set { _progressValue = value; OnPropertyChanged(); }
        }

        public string ProgressMessage
        {
            get => _progressMessage;
            private set { _progressMessage = value; OnPropertyChanged(); }
        }

        public ProgressStyle ProgressStyle
        {
            get => _progressStyle;
            private set { _progressStyle = value; OnPropertyChanged(); }
        }

        public ICommand DismissProgressCommand { get; }

        public ProgressSupportingViewModel()
        {
            DismissProgressCommand = new RelayCommand(DismissProgress);
        }

        public void UpdateProgress(int current, int total, string? message = null)
        {
            if (total <= 0) return;

            IsProgressVisible = true;
            ProgressStyle = ProgressStyle.Determinate;
            ProgressValue = (double)current / total;

            if (message != null)
                ProgressMessage = message;

            if (current >= total)
                EndProgress();
        }

        public void StartIndeterminate(string? message = null)
        {
            IsProgressVisible = true;
            ProgressStyle = ProgressStyle.Indeterminate;
            ProgressValue = 0;
            if (message != null)
                ProgressMessage = message;
        }

        public void EndProgress()
        {
            IsProgressVisible = false;
            ProgressValue = null;
            ProgressMessage = string.Empty;
        }

        private void DismissProgress()
        {
            EndProgress();
            OnProgressCancelled();
        }

        public event EventHandler ProgressCancelled = delegate { };

        protected virtual void OnProgressCancelled() =>
            ProgressCancelled?.Invoke(this, EventArgs.Empty);
    }
}
