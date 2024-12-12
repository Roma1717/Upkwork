//document.addEventListener('DOMContentLoaded', function () {
//    const minPriceInput = document.getElementById('adult-min-price');
//    const maxPriceInput = document.getElementById('adult-max-price');
//    const priceValues = document.getElementById('adult-price-values');

//    if (minPriceInput && maxPriceInput && priceValues) {
//        minPriceInput.addEventListener('input', updatePriceValues);
//        maxPriceInput.addEventListener('input', updatePriceValues);

//        function updatePriceValues() {
//            const adultMin = minPriceInput.value;
//            const adultMax = maxPriceInput.value;

//            priceValues.innerText = `${adultMin} - ${adultMax}`;
//        }
//    }



//    // Отправка данных через fetch при нажатии на кнопку
//    document.getElementById('apply-filter').addEventListener('click', () => {
//        // Сбор данных из формы
//        const idCountry = document.getElementById('Id_category').value;
//        const adultMin = document.getElementById('adult-min-price').value;
//        const adultMax = document.getElementById('adult-max-price').value;

//        // Формирование данных для отправки
//        const filterData = {
//            idCountry: idCountry,
//            priceAdultMin: adultMin,
//            priceAdultMax: adultMax,
//        };
//        console.log('Отправляемые данные:', filterData);

//        // Отправка данных через fetch
//        fetch('/Filter/Filter', {
//            method: 'POST',
//            headers: {
//                'Content-Type': 'application/json'
//            },
//            body: JSON.stringify(filterData)
//        })
//            .then(response => {
//                if (!response.ok) {
//                    return Promise.reject(new Error('Ошибка при фильтрации данных'));
//                }
//                return response.json(); // Преобразовать ответ в JSON
//            })
//            .then(data => {
//                console.log('Результаты фильтрации:', data);
//                dataDisplay(data); // Функция для отображения данных
//            })
//            .catch(error => {
//                console.error('Ошибка:', error);
//            });
//    });

//    function dataDisplay(data) {
//        // Найти контейнер для списка туров
//        const toursList = document.querySelector('.list-jobs');
//        toursList.innerHTML = ''; // Очистить старые данные

//        if (!data || data.length === 0) {
//            // Если нет данных, отображаем сообщение
//            const noToursMessage = '<h2>По данному фильтру нет туров</h2>';
//            toursList.innerHTML = noToursMessage;
//        } else {
//            // Если данные есть, создаем элементы для туров
//            data.forEach(tour => {
//                const tourItem = `
//        <div class="job-item">
//                        <div class="job-header">
//                            <h2>@job.Title</h2>
//                            <span class="job-location">@job.Data</span> <!-- Указание даты публикации или места (Data) -->
//                        </div>
//                        <div class="job-details">
//                            <p><strong>Описание:</strong> @job.Description</p>
//                            <p><strong>Продолжительность:</strong> @job.Duration</p> <!-- Продолжительность -->
//                            <p><strong>Цена:</strong> @job.Price</p> <!-- Цена -->
//                            <p><strong>Максимальное количество участников:</strong> @job.Max_participants</p> <!-- Количество участников -->
//                            <p><strong>Дата публикации:</strong> @job.Created_at.ToShortDateString()</p> <!-- Дата публикации -->
//                        </div>
//                        <div class="job-footer">
//                            <a href="/Jobs/Details/@job.Id" class="details-link">Подробнее</a>
//                        </div>
//                    </div>
//      `
//                toursList.innerHTML += tourItem; // Добавить тур в список
//            });
//        }
//    }



//});


document.addEventListener('DOMContentLoaded', function () {
    const minPriceInput = document.getElementById('adult-min-price');
    const maxPriceInput = document.getElementById('adult-max-price');
    const priceValues = document.getElementById('adult-price-values');
    const applyFilterButton = document.getElementById('apply-filter');

    if (minPriceInput && maxPriceInput && priceValues) {
        minPriceInput.addEventListener('input', updatePriceValues);
        maxPriceInput.addEventListener('input', updatePriceValues);

        function updatePriceValues() {
            const adultMin = minPriceInput.value;
            const adultMax = maxPriceInput.value;
            priceValues.innerText = `${adultMin} - ${adultMax}`;
        }
    }

    if (applyFilterButton) {
        applyFilterButton.addEventListener('click', () => {
            const idCountry = document.getElementById('Id_category')?.value || '';
            const adultMin = minPriceInput?.value || 0;
            const adultMax = maxPriceInput?.value || 2000;

            const filterData = {
                idCountry: idCountry,
                priceAdultMin: adultMin,
                priceAdultMax: adultMax,
            };

            console.log('Отправляемые данные:', filterData);

            fetch('/Home/ListOfJobs', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(filterData),
            })
                .then((response) => {
                    if (!response.ok) {
                        return Promise.reject(new Error('Ошибка при фильтрации данных'));
                    }
                    return response.json();
                })
                .then((data) => {
                    console.log('Результаты фильтрации:', data);
                    dataDisplay(data);
                })
                .catch((error) => {
                    console.error('Ошибка:', error);
                });
        });

    }
    function dataDisplay(data) {
        // Найти контейнер для списка туров
        const toursList = document.querySelector('.list-jobs');
        toursList.innerHTML = ''; // Очистить старые данные

        if (!data || data.length === 0) {
            // Если нет данных, отображаем сообщение
            const noToursMessage = '<h2>По данному фильтру нет туров</h2>';
            toursList.innerHTML = noToursMessage;
        } else {
            // Если данные есть, создаем элементы для туров
            data.forEach(tour => {
                const tourItem = `
    <div class="job-item">
        <div class="job-header">
            <h2>${tour.Title}</h2>
            <span class="job-location">${tour.Data}</span>
        </div>
        <div class="job-details">
            <p><strong>Описание:</strong> ${tour.Description}</p>
            <p><strong>Продолжительность:</strong> ${tour.Duration}</p>
            <p><strong>Цена:</strong> ${tour.Price}</p>
            <p><strong>Максимальное количество участников:</strong> ${tour.Max_participants}</p>
            <p><strong>Дата публикации:</strong> ${new Date(tour.Created_at).toLocaleDateString()}</p>
        </div>
        <div class="job-footer">
            <a href="/Jobs/Details/${tour.Id}" class="details-link">Подробнее</a>
        </div>
    </div>
`;

            });
        }
    }
});


