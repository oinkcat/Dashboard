jQuery(function () {
    // Maximize height of each chart in row
    $('.db-section').each(function (_, rowItem) {
        var maxHeight = 0;
        var indicatorContainers = $(rowItem).find('.db-indicator-content');

        indicatorContainers.each(function (_, container) {
            var jContainer = $(container);
            if (maxHeight < jContainer.height()) {
                maxHeight = jContainer.height();
            }
        });

        indicatorContainers.height(maxHeight);
    });

    // Resize charts
    window.dispatchEvent(new Event('resize'));
});