function recalculateIndexes(tableName, collectionName) {
    $(`#${tableName} tbody tr`).each(function (ix, el) {
        //console.log($(el));
        //console.log($(el).html());
        var elHtml = $(el).html();
        var pattern = collectionName + "\\[\\d+\\]";
        var regex = new RegExp(pattern, "gi");
        var replaced = elHtml.replace(regex, `${collectionName}[${ix}]`);
        console.log(replaced);
        $(el).html(replaced);
    });
}

function updateValue(input) {
    $(input).attr('value', $(input).val());
}

function getSelectedOptions(container, arr) {
    $(`#${container}`).find('option:selected').each(function (ix, element) {
        arr.push($(element).val());
    });
}