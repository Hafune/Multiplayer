using System.Collections;
using Core;
using Core.Services;
using Lib;
using UnityEngine;
using UnityEngine.UIElements;

public class InitializeSdk : MonoConstruct, IInitializeCheck
{
    [SerializeField] private UIDocument _loadingError;
    private LoadingErrorVT _errorVT;
    private PlayerDataService _dataService;
    private VisualElement _root;
    private bool _ignoreClicks = true;
    private bool _errorWasResolved;
    private bool _hasError;

    public bool IsInitialized { get; private set; }

    private void Awake()
    {
        _root = _loadingError.rootVisualElement;
        _root.DisplayNone();
        _errorVT = new LoadingErrorVT(_root);
        _errorVT.buttonRefetch.RegisterCallback<ClickEvent>(_ => TryReInitialize());
        _errorVT.buttonIgnore.RegisterCallback<ClickEvent>(_ => IgnoreLoadingFail());
    }

    private IEnumerator Start()
    {
        var skdController = context.Resolve<SdkService>();
        yield return skdController.Initialize(AfterSdkInit);
    }

    private void AfterSdkInit()
    {
        context.Resolve<LocalizationService>().OnInitialize += PreloadAfterLocalization;
        _dataService = context.Resolve<PlayerDataService>();
        _dataService.Initialize(ShowErrorWindow);
    }

    private void ShowErrorWindow(string error)
    {
        _root.DisplayFlex();
        _errorVT.errorMessage.text = error;
        _ignoreClicks = false;

        if (_hasError) 
            return;
        
        Debug.LogWarning("Cloud fetch error: Catch");
        _hasError = true;
        
        if (context.Resolve<SdkService>().GetLocale() != "ru")
            return;
        
        // Ошибка загрузки облачного сохранения
        // Если у вас уже есть прогресс в этой игре попробуйте загрузить данные снова нажав "Перезагрузить".
        //     Если вы никогда раньше не играли в эту нажмите "Пропустить", игра начнётся с самого начала.
        //     Загрузить данные
        //     Пропустить
        // If you already have progress in this game, click <color=#29B737>Try Load Data</color>.
        // If you have never played this game before, click <color=#BC3D44>Skip</color> to start a new game.
        _errorVT.errorTitle.text = "Ошибка загрузки облачного сохранения";
        _errorVT.warning.text = "Если у вас уже есть прогресс в этой игре попробуйте загрузить данные снова нажав <color=#29B737>Загрузить данные</color>.\n" +
                                "Если вы никогда раньше не играли в эту игру нажмите <color=#BC3D44>Пропустить</color>, игра начнётся с самого начала";
        _errorVT.buttonRefetch.text = "Загрузить данные";
        _errorVT.buttonIgnore.text = "Пропустить";
    }

    public void TryReInitialize()
    {
        if (_ignoreClicks)
            return;

        Debug.Log(nameof(TryReInitialize));
        _ignoreClicks = true;
        _errorVT.errorMessage.text = string.Empty;
        _errorWasResolved = true;
        _dataService.Initialize(ShowErrorWindow);
    }

    public void IgnoreLoadingFail()
    {
        if (_ignoreClicks)
            return;

        Debug.Log(nameof(IgnoreLoadingFail));
        _ignoreClicks = true;
        _errorWasResolved = false;
        _dataService.OnLoadSuccess();
    }

    private void PreloadAfterLocalization()
    {        
        context.Resolve<LocalizationService>().OnInitialize -= PreloadAfterLocalization;
        IsInitialized = true;
        _root.DisplayNone();
        Debug.Log("InitializeSdk Successful");
        
        if (_errorWasResolved)
            Debug.LogWarning("Cloud fetch error: Fixed");
    }
}
/*
--
Ошибка загрузки облачного сохранения
Если у вас уже есть прогресс в этой игре попробуйте загрузить данные снова нажав "Перезагрузить".
Если вы никогда раньше не играли в эту нажмите "Пропустить", игра начнётся с самого начала.
Перезагрузить
Пропустить
--
Error loading cloud save
If you already have progress in this game, click "Reload".
If you have never played this game before, click "Skip".
Reload
Skip
--
Памылка пры загрузцы хмарнага захавання
Калі ў вас ужо ёсць прагрэс у гэтай гульні, паспрабуйце загрузіць дадзеныя яшчэ раз, націснуўшы "перазагрузіць".
Калі вы ніколі раней не гулялі ў гэтую гульню, націсніце "Прапусціць", гульня пачнецца спачатку.
Перазагрузіць
Прапускаць
--
Erreur lors du chargement de la sauvegarde dans le cloud
Si vous avez déjà progressé dans ce jeu, essayez de télécharger à nouveau les données en cliquant sur "Recharger".
Si vous n'avez jamais joué à ce jeu auparavant, cliquez sur "Ignorer", le jeu recommencera depuis le début.
Recharger
Sauter
--
Fehler beim Laden des Cloud-Speichers
Wenn Sie bereits Fortschritte in diesem Spiel haben, versuchen Sie, die Daten erneut herunterzuladen, indem Sie auf "Laden" klicken.
Wenn Sie dieses Spiel noch nie gespielt haben, klicken Sie auf "Überspringen", das Spiel beginnt von vorne.
Laden
Überspringen
--
Galat saat memuat penyimpanan cloud
Jika Anda sudah memiliki progres dalam game ini, coba unduh kembali datanya dengan mengklik "Muat ulang".
Jika Anda belum pernah memainkan game ini sebelumnya, klik "Lewati", permainan akan dimulai dari awal.
Muat ulang
Skip
--
Galat saat memuat penyimpanan cloud
Se hai già dei progressi in questo gioco, prova a scaricare di nuovo i dati facendo clic su"Ricarica".
Se non hai mai giocato a questo gioco prima, fai clic su "Salta", il gioco inizierà dall'inizio.
Ricaricare
Saltare
--
Błąd ładowania chmury Zapisz
Jeśli masz już postępy w tej grze, spróbuj ponownie pobrać dane, klikając "Przeładuj".
Jeśli nigdy wcześniej nie grałeś w tę grę, kliknij "Pomiń", gra rozpocznie się od początku.
Przeładuj
Skip
--
Erro ao carregar o salvamento na nuvem
Se você já tem progresso neste jogo, Tente baixar os dados novamente clicando em"recarregar".
Se você nunca jogou este jogo antes, clique em "Pular", o jogo começará do início.
Recarregar
Ir
--
Error al cargar el guardado en la nube
Si ya tienes progreso en este juego, intenta descargar los datos nuevamente haciendo clic en "Recargar".
Si nunca antes has jugado a este juego, haz clic en "Omitir", el juego comenzará desde el principio.
Recargar
Saltar
--
Bulut kaydetme yüklenirken hata oluştu
Bu oyunda zaten ilerlemeniz varsa, "Yeniden Yükle" yi tıklayarak verileri tekrar indirmeyi deneyin.
Bu oyunu daha önce hiç oynamadıysanız, "Atla" yı tıklayın, oyun baştan başlayacaktır.
Yeniden yükle
Atlamak
--
Lỗi tải lưu đám mây
Nếu bạn đã có tiến trình trong trò chơi này, hãy thử tải xuống lại dữ liệu bằng cách nhấp vào "Tải lại".
Nếu bạn chưa bao giờ chơi trò chơi này trước đây, hãy nhấp vào "Bỏ qua", trò chơi sẽ bắt đầu từ đầu.
Tải lại
Bỏ qua
*/
