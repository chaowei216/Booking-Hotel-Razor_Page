var totalPrice = +0;
var dataList = [];

$(document).ready(function () {
    var startDate = $("#startDate");
    var endDate = $("#endDate");
    var totalPriceItem = $("#total-price");

    totalPriceItem.text("$" + totalPrice);

    startDate.change(function () {
        checkDateRange();
        handleChangeDate();
    });

    endDate.change(function () {
        checkDateRange();
        handleChangeDate();
    });

    $('#confirm').on("click", function () {
        handleClickConfirm();
    })

    updateEventClickAdd(startDate, endDate);

    const handleChangeDate = () => {
        var sDateVal = startDate.val();
        var eDateVal = endDate.val();

        $.ajax({
            method: 'GET',
            url: "/Room/?handler=UpdateRoomList",
            data: {
                startDate: sDateVal.toLocaleString(),
                endDate: eDateVal.toLocaleString()
            },
            contentType: 'application/json',
            success: function (response) {
                if (response.isSuccess && response.data) {
                    updateUI(response.data)
                }
            },
            error: function (xhr, status, error) {
                console.log("AJAX Error:");
                console.log("Status:", status);
                console.log("Error:", error);
                console.log("Response Text:", xhr.responseText);
            }
        })
    }

    const checkDateRange = () => {
        var start = new Date(startDate.val());
        var end = new Date(endDate.val());

        if (end < start) {
            alert("End date must be greater than start date!");
            endDate.val(startDate.val());
        }
    }

    const handleClickConfirm = () => {
        var token = $('input[name="__RequestVerificationToken"]').val();
        var id = $('#customerId').val();
        var bookingData = {
            details: dataList,
            price: totalPrice,
            customerId: id
        }

        // call ajax to add booking
        $.ajax({
            url: "/Room/?handler=Booking",
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            headers: {
                'RequestVerificationToken': token
            },
            data: JSON.stringify(bookingData),
            success: function (response) {
                if (response.isSuccess && response.data) {
                    updateUI(response.data);
                    console.log(response.data);
                    $('#startDate').val(new Date().toISOString().slice(0, 10));
                    $('#endDate').val(new Date().toISOString().slice(0, 10));
                    totalPrice = 0;
                    dataList = [];
                    updateDetail(dataList, totalPrice)
                    $("#confirm").prop('disabled', true);
                }
            },
            error: function (xhr, status, error) {
                console.log("AJAX Error:");
                console.log("Status:", status);
                console.log("Error:", error);
                console.log("Response Text:", xhr.responseText);
            }
        });
    }

    const updateUI = (rooms) => {
        var container = $("#container-room");
        container.empty();
        if (rooms != null && rooms.length == 0) {
            var divItem = $('<h4>')
            divItem.addClass("text-center");
            divItem.text("No item available");
            container.append(divItem);
        } else {
            rooms.forEach((item) => {
                var jsonItem = JSON.parse(item);
                var newItem = $(`
                                 <div class="col">
                                    <div class="card h-100">
                                        <div class="card-body d-flex flex-column justify-content-between">
                                            <div class="mb-3">
                                                <h5 class="card-title text-center" id="room-number-${jsonItem.id}">${jsonItem.number}</h5>
                                                <p class="card-text">${jsonItem.description}</p>
                                            </div>
                                            <div class="d-flex justify-content-between align-items-end">
                                                <p class="card-text mb-0"><span class="fw-bold">Price:</span> $<span id="room-price-${jsonItem.id}">${Math.trunc(jsonItem.price)} </span>/ day</p>
                                                <div>
                                                    <button id="${jsonItem.id}" class="btn btn-outline-dark me-2 add">Book</button>

                                                    <a href="/Room/Detail/${jsonItem.id}"  class="btn btn-outline-secondary">View Detail</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            `);
                container.append(newItem);
            });

            updateEventClickAdd(startDate, endDate);
        }
    }
});


const updateEventClickAdd = (startDate, endDate) => {
    var addBtn = $(".add");
    addBtn.on("click", function () {
        var itemId = $(this).attr('id');
        var itemPrice = $("#room-price-" + itemId).text();
        var itemNumber = $("#room-number-" + itemId).text();

        var existingItem = dataList.find(item => item.id === itemId);

        if (existingItem) {
            if (startDate.val() != existingItem.sDate || endDate.val() != existingItem.eDate) {
                dataList = dataList.filter(item => item.id !== itemId);

                totalPrice -= existingItem.price;
            }

            return;
        }

        var diffDays = getDaysDiff(new Date(endDate.val()), new Date(startDate.val())) + 1;

        dataList.push({
            id: itemId,
            number: itemNumber,
            sDate: startDate.val().toLocaleString(),
            eDate: endDate.val().toLocaleString(),
            price: parseFloat(itemPrice) * diffDays
        });

        totalPrice += parseFloat(itemPrice) * diffDays;
        updateDetail(dataList, totalPrice);
        $("#confirm").prop('disabled', false);
    });
}

const updateEventClickDelete = () => {
    var deleteBtn = $(".delete");

    deleteBtn.on("click", function () {
        var itemId = $(this).attr('id');

        var existingItem = dataList.find(item => item.id === itemId);

        if (existingItem) {
            dataList = dataList.filter(item => item.id !== itemId);

            if (dataList.length == 0) {
                $("#confirm").prop('disabled', true);
            }

            totalPrice -= existingItem.price;

            updateDetail(dataList, totalPrice);
        }
    })
}

const updateDetail = (list, price) => {
    var container = $(".detail-item");
    var totalPrice = $("#total-price");

    container.empty();

    if (list.length == 0) {
        container.append($(`<p class="text-xl-center">Booking something!</p>`))
    } else {
        list.forEach(item => {
            var newItem = $(`<div class="row mb-3 justify-content-center">
                          <div class="col-md-8 col-lg-6">
                            <div class="booking-details rounded border p-3 overflow-hidden">
                              <div class="d-flex justify-content-between align-items-center">
                                <div>
                                  <span class="fw-bold">Room Number:</span> ${item.number} - <span class="fw-bold">Start Date:</span> ${item.sDate} <span class="fw-bold">End Date:</span> ${item.eDate}
                                </div>
                                <button id="${item.id}" class="btn btn-danger btn-sm delete">Delete</button>
                              </div>
                            </div>
                          </div>
                        </div>`);

            container.append(newItem);
        })
        updateEventClickDelete();
    }

    totalPrice.text("$" + price);

}

const getDaysDiff = (date1, date2) => {
    const oneDay = 24 * 60 * 60 * 1000;
    const diffDays = Math.round(Math.abs((date1 - date2) / oneDay));
    return diffDays;
}






