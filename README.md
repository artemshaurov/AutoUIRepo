# AutoUIRepo

## Descr
Инструмент для создания внутриигровых интерфейсов напрямую из сериализованных классов; Предназначен для быстрого создания инструментов для тестирования (например, для редактирования данных пользователя напрямую);

## Setup
Пример применения можно найти на сцене AutoUI/Samples/AutoUITest.scene. Тулза находится в разработке, в тестовую сцену не завезли адаптивную верстку, поэтому рекомендуются запускать в портретной ориентации с разрешением не менее 1920х1080; 
После запуска на сцене появятся две кнопки. Клик по кнопке "Create" создаст интерфейс из объекта TestClass m_TestClass, описанного в компоненте AutoUITest, который прикреплен к объекту на сцене Canvas/AutoUI. Клик по кнопке "Deserialize" считает данные из созданного интерфейса и запишет их в объект TestClass m_Deserialized, который находится в том же компоненте.  

## ToDo 
- Адаптивная верстка;
- Обработка рекурсии;

## Screenshots 
Сериализованный объект в инспекторе:
![AutoUI-inspector](https://github.com/artemshaurov/AutoUIRepo/assets/109016572/f42a901a-e5eb-4ac9-ae6d-972b81150bdb)  

Автоматически-созданный UI для объекта TestClass m_TestClass:
![AutoUI-ui](https://github.com/artemshaurov/AutoUIRepo/assets/109016572/f1acec0e-a9e1-4a5d-be22-3c223c4eea3b)

Поддержка Enum: 
![AutoUI-ui2](https://github.com/artemshaurov/AutoUIRepo/assets/109016572/d4a459b4-a77c-4495-a93e-3612bdec320a)

Поддержка вложенных объектов и массивов: 
![AutoUI-ui3](https://github.com/artemshaurov/AutoUIRepo/assets/109016572/c3aa8bf1-4e7b-4f1d-88a3-c252186fda40)

