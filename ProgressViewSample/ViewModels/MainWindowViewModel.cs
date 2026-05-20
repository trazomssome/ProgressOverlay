using ProgressViewSample.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProgressViewSample.ViewModels
{
    public class MainWindowViewModel : ProgressSupportingViewModel
    {
        public ICommand LoadDataCommand { get; }

        public MainWindowViewModel()
        {
            LoadDataCommand = new RelayCommand(async () => await LoadDataAsync());
        }

        private async Task LoadDataAsync()
        {
            try
            {
                StartIndeterminate("데이터 로딩 준비 중...");

                await Task.Delay(500);

                var items = await FetchDataWithProgressAsync();

                EndProgress();
            }
            catch (Exception ex)
            {
                EndProgress();
                MessageBox.Show($"오류: {ex.Message}");
            }
        }

        private async Task<List<string>> FetchDataWithProgressAsync()
        {
            var result = new List<string>();
            int total = 100;

            for (int i = 1; i <= total; i++)
            {
                // 진행률 업데이트
                UpdateProgress(i, total, $"{i}/{total} 항목 처리 중...");

                // 실제 작업 시뮬레이션
                await Task.Delay(50);
                result.Add($"Item {i}");
            }

            return result;
        }
    }
}
