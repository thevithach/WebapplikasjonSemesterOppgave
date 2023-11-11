$(document).ready(function () {
    function updateRoleSectionColor(roleName) {
        var roleRows = $('.checklist-row.' + roleName);
        var allChecked = true;
        var hasBørSkiftes = false;
        var hasDefekt = false;
        var bool = false;

        roleRows.each(function () {
            var value = $(this).find('input[type=radio]:checked').val();
            if (value !== "OK" && value !== "Bør_skiftes" && value !== "Defekt" && value !== "true"){
                allChecked = false;
            }
            if (value === "Bør_skiftes"){
                hasBørSkiftes = true;
            }
            if (value === "Defekt") {
                hasDefekt = true;
            }
            if (value === "false"){
                bool = false;
            }

        });

        var roleHeader = $('.' + roleName + '-header');

        if (allChecked) {
            roleHeader.removeClass('orange-bg red-bg').addClass('green-bg');
        } else {
            roleHeader.removeClass('green-bg');

            if (hasBørSkiftes) {
                roleHeader.removeClass('red-bg').addClass('orange-bg');
            } else if (hasDefekt) {
                roleHeader.removeClass('orange-bg').addClass('red-bg');
            } else if (bool) {
                roleHeader.removeClass('orange-bg').addClass('red-bg');
            }
        }
    }

    function updateRowColor(row, value) {
        row.removeClass('green-bg orange-bg red-bg').addClass(GetRowColor(value));
    }

    function GetRowColor(condition) {
        if (condition == "OK") {
            return "green-bg";
        } else if (condition == "Bør_skiftes") {
            return "orange-bg";
        } else if (condition == "Defekt") {
            return "red-bg";
        } else if (condition == "true") {
            return "green-bg";
        } else if (condition == "false") {
            return "red-bg";
        }
        return ""; // Default, no background color
    }

    $('.checklist-row input[type=radio]').change(function () {
        var row = $(this).closest('.checklist-row');
        var value = $(this).val();
        updateRowColor(row, value);

        // Update section color for each role
        updateRoleSectionColor('mekaniker');
        updateRoleSectionColor('elektro');
        updateRoleSectionColor('hydraulic');
        // Add more roles as needed
    });

    // Initial check when the page loads
    updateRoleSectionColor('mekaniker');
    updateRoleSectionColor('elektro');
    updateRoleSectionColor('hydraulic');
    // Add more roles as needed
});