
var benefitPage = function () {

    var urls = {},
		$form,
        $tbody;

	function _addDependent() {
		var $tr = $tbody.find('tr'),
			count = $tr.length,
			lastOk = $tr.last().find('input').val() !== '';

		if (lastOk && $form.valid()) {
			$tbody
				.append($('<tr>')
					.append($('<td>')
						.append($('<input class="text-box single-line valid" id="Dependents_' + count + '_" name="Dependents[' + count + ']" type="text" />'))
					)
				);
		}
    }

    function _calculateBenefits() {
    	if ($form.valid()) {
    		$.post(urls.calculate, $form.serialize(), function (data) {
    			$('#benefitYear').html('$' + data.BenefitYear);
    			$('#benefitCheck').html('$' + data.BenefitCheck);
    			$('#totalYear').html('$' + data.TotalYear);
    			$('#totalCheck').html('$' + data.TotalCheck);
    		});
    	}
    }

    function _removeDependent(e) {
        $tbody.find('tr').last().remove();
    }

    function benefitPageStartup() {
        $('#addDependent').on('click', function () {
        	_addDependent();

        	return false;
        });

        $('#calculate').on('click', function () {
        	_calculateBenefits();

        	return false;
        });

        $('#removeDependent').on('click', function () {
            _removeDependent();

            return false;
        });

        $urls = $('#urls');
        urls.calculate = $urls.attr('data-url-calculate');

        $form = $('form');

        $tbody = $('#dependentTable').find('tbody');
    }

    // public methods
    return {
        benefitPageStartup : benefitPageStartup
    };
}();
