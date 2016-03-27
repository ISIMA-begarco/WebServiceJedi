function tournoiSelected() {
    var tournoiName = $('#Tournoi').val();
    $.ajax({
        url: '/Game/TournoiSelected',
        type: "GET",
        dataType: "JSON",
        data: { Tournoi: tournoiName },
        success: function (jedis) {
            $("#Jedi").html(""); // clear before appending new list
            $.each(jedis, function (i, jedi) {
                $("#Jedi").append(
                    $('<option></option>').val(jedi.Id).html(jedi.Nom));
            });
        }
    });
}