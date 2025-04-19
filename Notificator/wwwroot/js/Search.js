$(function() {
    // 페이지 로드 시 각 채널의 팔로우 상태 확인
    $('.follow-button').each(function() {
        const channelId = $(this).data('channel-id');
        checkFollowStatus(channelId);
    });
});

function checkFollowStatus(channelId) {
    $.ajax({
        url: '/Follow/IsFollowing',
        type: 'GET',
        data: { channelId: channelId },
        success: function(response) {
            const button = $(`.follow-button[data-channel-id="${channelId}"]`);
            updateButtonState(button, response);
        },
        error: function(xhr, status, error) {
            console.error('팔로우 상태 확인 실패:', error);
            const button = $(`.follow-button[data-channel-id="${channelId}"]`);
            button.find('.follow-text').text('상태확인실패');
        }
    });
}

function updateButtonState(button, isFollowing) {
    if (isFollowing) {
        button.removeClass('bg-indigo-600 hover:bg-indigo-700')
              .addClass('bg-red-600 hover:bg-red-700');
        button.find('.follow-text').text('알람끄기');
    } else {
        button.removeClass('bg-red-600 hover:bg-red-700')
              .addClass('bg-indigo-600 hover:bg-indigo-700');
        button.find('.follow-text').text('알람추가');
    }
}

function toggleFollow(channelId) {
    const button = $(`.follow-button[data-channel-id="${channelId}"]`);
    button.prop('disabled', true); // 버튼 비활성화
    
    $.ajax({
        url: '/Follow/ToggleFollow',
        type: 'POST',
        data: { channelId: channelId },
        success: function(response) {
            if (response.returnCode != 0) {
                alert(response.returnMsg);
                return;
            }
            checkFollowStatus(channelId);
        },
        error: function(xhr, status, error) {
            alert('알람 설정 중 오류가 발생했습니다.');
            console.error('알람 설정 실패:', error);
        },
        complete: function() {
            button.prop('disabled', false); // 버튼 다시 활성화
        }
    });
}