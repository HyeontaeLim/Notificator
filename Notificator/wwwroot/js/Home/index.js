$(function(){
    $('#platformSelector').on('change', function() {
        const $searchInput = $('#chzzk-search-input');
        if ($(this).val() === '치지직') {
            $searchInput.attr('placeholder', '치지직 스트리머 검색');
        } else if ($(this).val() === '아프리카') {
            $searchInput.attr('placeholder', '아프리카 스트리머 검색');
        } else {
            $searchInput.attr('placeholder', '스트리머 검색');
        }
    });

    // 페이지네이션 버튼 이벤트 리스너
    const $prevPageBtn = $('#prevPageBtn');
    const $nextPageBtn = $('#nextPageBtn');
        
    // 다음 페이지 버튼 클릭 이벤트
    $nextPageBtn.on('click', function() {
        // 여기에 다음 페이지 로드 기능 구현
        loadLiveList();
        console.log('다음 페이지 버튼 클릭됨');
    });
});

function loadLiveList() {
    let nextPage = $('#nextPageBtnText').text();
    let data = nextPage ? { next: nextPage } : {};
    
    $.ajax({
        url: '/getLiveList',
        type: 'GET',
        dataType: 'html',
        async: true,
        data: data,
        success: function(response) {
            $('#liveListContainer').html(response);
        },
        error: function(xhr, status, error) {
            console.error('Live 목록 로드 실패:', error);
        }
    });
}
