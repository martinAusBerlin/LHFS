/*

$().ready(function () {

	$('input[name="TypeRatingIDs"]').click(function () {

		var allVals = [];
		$('input[name="TypeRatingIDs"]:checked').each(function () {
			allVals.push($(this).val());
		});

		$.ajax({
			url: $('#TypeRatingIDs_1').attr('data-val-remote-url'),
			dataType: 'json',
			data: {
				id: allVals.join(',')
			},
			success: function (e) {

				alert("geht nicht");

				
				if (e) {
					$('span[data-valmsg-for="TypeRatingIDs"]').attr('class', 'field-validation-valid');
				} else {
					$('span[data-valmsg-for="TypeRatingIDs"]').text($('#TypeRatingIDs_1').attr('data-val-remote'));
					$('span[data-valmsg-for="TypeRatingIDs"]').attr('class', 'field-validation-error');
				}
				
			}
		});
	});
});
*/