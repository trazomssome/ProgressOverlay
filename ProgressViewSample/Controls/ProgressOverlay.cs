using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProgressViewSample.Controls
{
    public class ProgressOverlay : Control
    {
        // 템플릿 파트 이름 상수
        private const string PART_ProgressBorder = "PART_ProgressBorder";
        private const string PART_CancelButton = "PART_CancelButton";

        private Border _progressBorder;
        private Button _cancelButton;

        // 정적 생성자 - 기본 스타일 키 설정
        static ProgressOverlay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressOverlay),
                new FrameworkPropertyMetadata(typeof(ProgressOverlay)));
        }

        // 1. IsProgressVisible - 프로그레스 표시 여부
        public static readonly DependencyProperty IsProgressVisibleProperty =
            DependencyProperty.Register(nameof(IsProgressVisible), typeof(bool),
            typeof(ProgressOverlay), new PropertyMetadata(false));

        public bool IsProgressVisible
        {
            get => (bool)GetValue(IsProgressVisibleProperty);
            set => SetValue(IsProgressVisibleProperty, value);
        }

        // 2. ProgressValue - 현재 진행률 (0~1, null이면 Indeterminate)
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register(nameof(ProgressValue), typeof(double?),
            typeof(ProgressOverlay), new PropertyMetadata(null));

        public double? ProgressValue
        {
            get => (double?)GetValue(ProgressValueProperty);
            set => SetValue(ProgressValueProperty, value);
        }

        // 3. ProgressMessage - 진행 상태 메시지
        public static readonly DependencyProperty ProgressMessageProperty =
            DependencyProperty.Register(nameof(ProgressMessage), typeof(string),
            typeof(ProgressOverlay), new PropertyMetadata(string.Empty));

        public string ProgressMessage
        {
            get => (string)GetValue(ProgressMessageProperty);
            set => SetValue(ProgressMessageProperty, value);
        }

        // 4. ProgressStyle - 프로그레스 스타일 (Indeterminate/Determinate)
        public static readonly DependencyProperty ProgressStyleProperty =
            DependencyProperty.Register(nameof(ProgressStyle), typeof(ProgressStyle),
            typeof(ProgressOverlay), new PropertyMetadata(ProgressStyle.Indeterminate));

        public ProgressStyle ProgressStyle
        {
            get => (ProgressStyle)GetValue(ProgressStyleProperty);
            set => SetValue(ProgressStyleProperty, value);
        }

        // 5. DismissCommand - 취소 명령
        public static readonly DependencyProperty DismissCommandProperty =
            DependencyProperty.Register(nameof(DismissCommand), typeof(ICommand),
            typeof(ProgressOverlay), new PropertyMetadata(null));

        public ICommand DismissCommand
        {
            get => (ICommand)GetValue(DismissCommandProperty);
            set => SetValue(DismissCommandProperty, value);
        }

        // 템플릿 적용 시 호출
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // 기존 이벤트 핸들러 제거
            if (_cancelButton != null)
            {
                _cancelButton.Click -= OnCancelClick;
            }

            // 템플릿에서 파트 가져오기
            _progressBorder = GetTemplateChild(PART_ProgressBorder) as Border;
            _cancelButton = GetTemplateChild(PART_CancelButton) as Button;

            // 새 이벤트 핸들러 등록
            if (_cancelButton != null)
            {
                _cancelButton.Click += OnCancelClick;
            }
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            if (DismissCommand?.CanExecute(null) == true)
            {
                DismissCommand?.Execute(null);
            }
        }
    }

    // ProgressStyle 열거형
    public enum ProgressStyle
    {
        Indeterminate,  // 무한 회전 모드
        Determinate     // 퍼센트 표시 모드
    }
}