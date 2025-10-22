using System.Collections.Generic;
using Core.ExternalEntityLogics;
using Lib;

namespace Core.Services
{
    public class ActionRunesController : MonoConstruct
    {
        private ActionRuneMarker[] _runes;
        private AbstractEntityAction _entityAction;
        private ActionBarService _actionService;
        private string _actionName;
        private ActionRuneMarker _selectedRune;

        public IReadOnlyList<ActionRuneMarker> Runes => _runes;

        private void Awake()
        {
            _entityAction = GetComponentInParent<AbstractEntityAction>();
            _runes = transform.GetComponentsInChildren<ActionRuneMarker>(true);
            _actionName = _entityAction.Key;
            _actionService = context.Resolve<ActionBarService>();
            _actionService.OnChange += Refresh;
            Refresh();
        }

        public void SetActiveRune(ActionRuneMarker rune)
        {
            _actionService.SetRune(_actionName, rune?.Key ?? string.Empty);
            Refresh();
        }

        public bool TryGetSelectedRune(out ActionRuneMarker rune) => (rune = _selectedRune) != null;

        private void Refresh()
        {
            _selectedRune = null;
            foreach (var rune in _runes)
                rune.gameObject.SetActive(false);

            var runeKey = _actionService.GetSelectedRuneKey(_actionName);
            if (string.IsNullOrEmpty(runeKey))
                return;

            foreach (var rune in _runes)
            {
                if (rune.Key != runeKey)
                    continue;

                rune.gameObject.SetActive(true);
                _selectedRune = rune;
                break;
            }
        }

        private void OnDestroy() => _actionService.OnChange -= Refresh;
    }
}