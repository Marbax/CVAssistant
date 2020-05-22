# CVAssistant
 App to make CV creation easier ***Morozov M.E.***

<dev ><img src="gitAnim/rainbow.gif" width="100%" height="25" margin = "200" align="center">
</dev>

<img src="https://octodex.github.com/images/daftpunktocat-thomas.gif" width="120" align="right">


<details><summary> Technical Task  </summary><p>

### Создать приложение «Мастер по созданию резюме».
  **Основная задача приложения:**
  - [ ] создание утилиты для формирования резюме.  
 
  **Интерфейс приложения должен предоставлять такие возможности:**
 - [ ] На первом шаге пользователь выбирает шаблон создаваемого резюме.
 - [ ] Приложение при старте должно предоставить на выбор 5 или более шаблонов.
 - [ ] Заполняемые секции резюме зависят от типа шаблона.
 - [ ] Обязательные данные для любого резюме: ФИО, дата рождения, фотография, контактный телефон, e-mail, опыт работы.
 - [ ] На последнем шаге пользователю предоставляется возможность сохранить созданное резюме в файл формата: .pdf, .doc, .docx.
 - [ ] В программе должно быть реализовано автосохранение.
 - [ ] На каждом из этапов создания резюме существует возможность закрыть приложение. 
    - [ ] После нового запуска программа должна переходить на последний активный этап в создании резюме прошлого сеанса работы.
 - [ ] В любой момент времени пользователь может выбрать создание нового резюме с нуля.
 - [ ] Пошаговый интерфейс по созданию резюме.Каждый шаг – отдельное окно.
 - [ ] Конкретное окно должно позволять пользователю вернуться на предыдущий шаг, перейти на следующий шаг и т. д.

---

<details><summary> Comments </summary><p>

> - json для создания шаблона резюме
> - pdf тоже xml , должны быть бесплатные библиотеки для работы с ним
> - данные сохраняются в файл , при запуске выгружаются
> - класс отвечающий за работу всей программы , при переходе между окнами созранять данные в класс
> - можно сделать с одним окном по пунктам с сохранением - выплевывает пдфку
> - нужно строить паттерн(из IOS разработки) , допустим клаасс форм координейтор ,который хранит ссылки на все формы , методы которые прячут и отборажают формы и сделать единый интерфейс взаимодействия со всеми формами
> - App.xml StartupUri (апликейшен ран) можно запускать нужное окно , запоминать последнюю форму в классе


</p></details>

---

<details><summary> Plan </summary><p>

- [x] Просмотреть теорию на наличие полезныех данных.
- [ ] Изучить json для создания шаблонов резюме.
    - [ ] Создать Шаблон резюме(используется DotLiquid и flowDocument).
- [x] Поискать библиотеки для работы с PDF(Используется PDFSharp).
- [ ] Спланировать архитектуру по MVVM с одним шаблоном для начала (не сильно заморачиваясь, с одним окном для всего заполнения резюме).
- [ ] Добавить интернационализацию.
    - [ ] Придумать класс модели (Резюме).
    - [ ] Придумать View (https://smart-hr.com.ua/ua/applicants/create-resume хороший пример для пунктов конструктора)
        - [ ] Набросать ее в WinForms.
        - [ ] Использовать:
            - [ ] MenuStrip для создания нового резюме в любой момент и различных операций.
    - [ ] Придумать ViewModel
        - [ ] Добавить сохранение в .pdf.
        - [ ] Добавить сохранение в .doc и .docx.
- [ ] Добавить биндинги для сохранения данных в обьект класса ,на случай закрытия или вылета приложения.
- [ ] Добавить сериализацию как метод хранения данных (возможно другие варианты)
- [ ] Протестировать хорошенько работу
    - [ ] Внести коррективы
- [ ] Привесты MVVM к правильному виду
- [ ] Добавить стилей анимаций и тд.

### Если хватит времени 
- [ ] Придумать как и на какие формы разделить конструктор
- [ ] Почитать про класс-паттерн из IOS разработки FormCoordinator
- [ ] Добавить новый проект с новой ,улучшенной View
- [ ] Реализовать базово класс на нескольких этапах конструктора и проверить работоспособность
- [ ] Закончить проект с множеством этапов на разных окнах(формах)

</p></details>

</p></details>

