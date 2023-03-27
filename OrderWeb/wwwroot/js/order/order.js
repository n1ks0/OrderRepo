function addOrderItem() {
    var orderId = 0;
    console.log($('#Id').val());
    if ($('#Id').val() != 0 && $('#Id').val() != undefined) {
        orderId = $('#Id').val();
    }

    var row =
        `<tr>
            <input type="hidden" class="orderId" name="OrderItems[0].OrderId"
            onchange="updateValue(this)" value="${orderId}" required />
            <td>
                <input type="text" data-val="true" onchange="updateValue(this)"
                    data-val-required="The Наименование field is required."
                    id="OrderItems_0__Name" name="OrderItems[0].Name" value="">
            </td>
            <td>
                <input type="text" data-val="true" onchange="updateValue(this)"
                    data-val-number="The field Количество must be a number."
                    data-val-required="The Количество field is required."
                    id="OrderItems_0__Quantity" name="OrderItems[0].Quantity" value="">
            </td>
            <td>
                <input type="text" data-val="true" onchange="updateValue(this)"
                    data-val-required="The Единица измерения field is required."
                    id="OrderItems_0__Unit" name="OrderItems[0].Unit" value="">
            </td>
            <td>
                <button type="button" class="btn" onclick="removeOrderItem(this)">Удалить</button>
            </td>
        `
    $("#orderItemsTable tbody").append(row);
    recalculateIndexes("orderItemsTable", "OrderItems");
}

function removeOrderItem(item) {
    var id = $(item).closest('tr').find('.id').val();

    if (id == undefined || id == 0) {
        $(item).closest('tr').remove();
    }
    else {
        var item = $(item).closest('tr');
        $(item).hide();
        $(item).find('.isDeleted').val('true');
    }
}

function deleteOrder(id) {
    if (confirm("Вы действительно хотите удалить заказ?")) {
        $.ajax({
            url: '/Order/Delete',
            method: 'delete',
            data: { id: id },
            success: function () {
                window.location.href = `/Order/Index`;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(textStatus);
            } 
        });
    }
}

function doFilter() {
    var start = $('#Start').val();
    var end = $('#End').val();
    var providers = [];
    var numbers = [];
    getSelectedOptions('Numbers', numbers);
    getSelectedOptions('Providers', providers);
    $.ajax({
        url: '/Order/Filter',
        method: 'post',
        dataType: 'html',
        data: {
            start: start,
            end: end,
            providerIds: providers,
            orderNumbers : numbers
        },
        success: function (data) {
            $('#orders').html(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus);
        } 
    });
}
