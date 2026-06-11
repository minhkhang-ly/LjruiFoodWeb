(function() {
    function init() {
    // Tạo cấu trúc HTML cho Chatbot và thêm vào body
    var chatbotHtml = `
        <div id="chatbot-wrapper" class="chatbot-wrapper">
            <!-- Nút bong bóng chat -->
            <button id="chatbot-toggle-btn" class="chatbot-toggle-btn" aria-label="Mở trợ lý ảo">
                <i class="fa-solid fa-comments"></i>
                <span class="chatbot-pulse"></span>
            </button>

            <!-- Hộp thoại chat -->
            <div id="chatbot-box" class="chatbot-box">
                <div class="chatbot-header">
                    <div class="chatbot-header__info">
                        <div class="chatbot-header__avatar">
                            <i class="fa-solid fa-robot"></i>
                        </div>
                        <div>
                            <p class="chatbot-header__title">Trợ Lý LjruiFood</p>
                            <span class="chatbot-header__status"><span class="status-dot"></span> Đang trực tuyến</span>
                        </div>
                    </div>
                    <button id="chatbot-close-btn" class="chatbot-close-btn" aria-label="Đóng">&times;</button>
                </div>

                <div id="chatbot-messages" class="chatbot-messages">
                    <div class="chat-msg bot">
                        <div class="chat-msg__bubble">
                            Chào bạn! Tôi là trợ lý ảo LjruiFood. Tôi có thể giúp bạn tìm kiếm món ăn hoặc tra cứu trạng thái đơn hàng của bạn. Bạn muốn hỏi gì hôm nay?
                        </div>
                    </div>
                </div>

                <div class="chatbot-input-area">
                    <input type="text" id="chatbot-input-field" placeholder="Nhập câu hỏi của bạn..." autocomplete="off" />
                    <button id="chatbot-send-btn" aria-label="Gửi"><i class="fa-solid fa-paper-plane"></i></button>
                </div>
            </div>
        </div>
    `;

    document.body.insertAdjacentHTML("beforeend", chatbotHtml);

    // Lấy các phần tử DOM
    var wrapper = document.getElementById("chatbot-wrapper");
    var toggleBtn = document.getElementById("chatbot-toggle-btn");
    var closeBtn = document.getElementById("chatbot-close-btn");
    var chatBox = document.getElementById("chatbot-box");
    var messagesContainer = document.getElementById("chatbot-messages");
    var inputField = document.getElementById("chatbot-input-field");
    var sendBtn = document.getElementById("chatbot-send-btn");

    // Xử lý bật/tắt cửa sổ chat
    toggleBtn.addEventListener("click", function () {
        chatBox.classList.add("is-open");
        toggleBtn.classList.add("is-hidden");
        inputField.focus();
        scrollToBottom();
    });

    closeBtn.addEventListener("click", function () {
        chatBox.classList.remove("is-open");
        toggleBtn.classList.remove("is-hidden");
    });

    // Xử lý gửi tin nhắn
    function sendMessage() {
        var text = inputField.value.trim();
        if (!text) return;

        // Thêm tin nhắn của User
        appendMessage(text, "user");
        inputField.value = "";

        // Thêm trạng thái Đang trả lời... (typing indicator)
        var typingId = appendTypingIndicator();

        // Gửi AJAX request lên server
        fetch("/api/chatbot", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ message: text })
        })
        .then(function (res) {
            return res.json();
        })
        .then(function (data) {
            // Xóa typing indicator
            removeTypingIndicator(typingId);
            // Thêm phản hồi của Bot
            appendMessage(data.response, "bot");
        })
        .catch(function (err) {
            removeTypingIndicator(typingId);
            appendMessage("Rất tiếc, hệ thống gặp lỗi kết nối. Bạn vui lòng thử lại sau nhé!", "bot");
            console.error("Chatbot error:", err);
        });
    }

    sendBtn.addEventListener("click", sendMessage);

    inputField.addEventListener("keydown", function (e) {
        if (e.key === "Enter") {
            sendMessage();
        }
    });

    // Hàm thêm tin nhắn vào container
    function appendMessage(htmlContent, sender) {
        var msgDiv = document.createElement("div");
        msgDiv.className = "chat-msg " + sender;
        msgDiv.innerHTML = `<div class="chat-msg__bubble">${htmlContent}</div>`;
        messagesContainer.appendChild(msgDiv);
        scrollToBottom();
    }

    // Hàm thêm hiệu ứng gõ chữ
    function appendTypingIndicator() {
        var id = "typing-" + Date.now();
        var msgDiv = document.createElement("div");
        msgDiv.className = "chat-msg bot typing-indicator-msg";
        msgDiv.id = id;
        msgDiv.innerHTML = `
            <div class="chat-msg__bubble typing-bubble">
                <span class="dot"></span>
                <span class="dot"></span>
                <span class="dot"></span>
            </div>
        `;
        messagesContainer.appendChild(msgDiv);
        scrollToBottom();
        return id;
    }

    // Hàm xóa hiệu ứng gõ chữ
    function removeTypingIndicator(id) {
        var el = document.getElementById(id);
        if (el) {
            el.remove();
        }
    }

    // Tự động cuộn xuống cuối
    function scrollToBottom() {
        messagesContainer.scrollTop = messagesContainer.scrollHeight;
    }
    }
    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", init);
    } else {
        init();
    }
})();
