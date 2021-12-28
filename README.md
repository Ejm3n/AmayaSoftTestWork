# AmayaSoftTestWork
Тестовое задание на работу. В игре необходимо нажать на ответ который появится сверху.
Инструкция по добавлению мода в игру:
1. В проекте в папке Resources/GameModes создать папку со спрайтами, а в папке Resources/ScriptableObjects/GameModes добавить scriptable object, где будут лежать все используемые спрайты мода, название
2. На сцене MainMenu на канвасе найти скрипт choosingmode, при добавлении новой кнопки в button OnClick() добавить ссылку на choosingmode, там выбрать метод OnModeChooseClick, вписать туда название мода которое совпадает со scriptable object в папке Resources/ScriptableObjects/GameModes;
