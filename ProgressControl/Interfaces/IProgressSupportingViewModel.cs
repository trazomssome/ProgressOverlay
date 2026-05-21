using System;
using System.ComponentModel;
using System.Windows.Input;


namespace ProgressControl.Interfaces
{
    public interface IProgressSupportingViewModel : INotifyPropertyChanged
    {
        bool IsProgressVisible { get; }

        double? ProgressValue { get; }

        string ProgressMessage { get; }

        ProgressStyle ProgressStyle { get; }

        ICommand DismissProgressCommand { get; }

        void UpdateProgress(int current, int total, string message = null);

        // 프로그레스 시작 (Indeterminate)
        void StartIndeterminate(string message = null);

        // 프로그레스 종료
        void EndProgress();
    }

    public enum ProgressStyle
    {
        Indeterminate,  // 무한 회전 형태
        Determinate     // 퍼센트 표시
    }
}