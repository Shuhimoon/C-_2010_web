<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPage.aspx.cs" Inherits="MyWorkWebsite.UploadPagesFile.UploadPage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Upload Page</title>
    <link href="~/Styles/SubPage.css" rel="Stylesheet" />
    <link rel="icon" type="image/x-icon" href="~/Styles/cat.png" />
    <!--[if IE]><link href="IE_SubPage/IESubPage.css" type="text/css" rel="Stylesheet"/><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
        <div id="dropZone">
            <p>將檔案或元素拖到這裡</p>
        </div>
        <script>
            const dropZone = document.getElementById('dropZone');
            let storedFiles = []; // 儲存檔案和對應的input
           
            // 在 dropZone 上新增 'dragover' 事件監聽器，當滑鼠拖東西經過區域時觸發，她會知道你現在有沒有拉東西到那個框框裡，有的話透過dragover 改變顏色
            dropZone.addEventListener('dragover', (event) => {
                // 防止瀏覽器預設行為
                event.preventDefault();
                // 新增這個類別，透過CSS控制樣子
                dropZone.classList.add('dragover');
            });
            dropZone.addEventListener('dragleave', () => {
                // 移除剛剛丟進來的觸發樣式
                dropZone.classList.remove('dragover');
            });
            dropZone.addEventListener('drop', (event) => {
                event.preventDefault();
                dropZone.classList.remove('dragover');
                // 不清空顯示區，讓新檔案追加到現有檔案後面
                if (dropZone.innerHTML.trim() === '<p>將檔案或元素拖到這裡</p>') {
                    dropZone.innerHTML = ''; // 只在初始時清空
                }
                const files = event.dataTransfer.files;
                if (files.length > 0) {
                    for (let i = 0; i < files.length; i++) {
                        const file = files[i];
                        const fileItem = document.createElement('div');
                        fileItem.classList.add('file-item');
                        // 創建 icon 元素
                        const icon = document.createElement('div');
                        icon.classList.add('file-icon');
                        let iconClass = 'default';
                        let iconContent = '\u25C6'; // 預設 icon
                        if (file.type.startsWith('image/')) {
                            iconClass = 'img';
                            const img = document.createElement('img');
                            img.src = URL.createObjectURL(file);
                            img.classList.add('file-icon', 'img');
                            icon.appendChild(img);
                        } else if (file.type === 'application/pdf') {
                            iconClass = 'pdf';
                            iconContent = 'PDF';
                        } else if (file.type === 'application/msword' || file.type === 'application/vnd.openxmlformats-officedocument.wordprocessingml.document') {
                            iconClass = 'doc';
                            iconContent = 'DOC';
                        }
                        if (!icon.querySelector('img')) {
                            icon.innerHTML = iconContent;
                        }
                        icon.classList.add(iconClass);
                        fileItem.appendChild(icon);
                        // 檔名和大 小（改成textbox讓使用者編輯）
                        const fileInfo = document.createElement('div');
                        fileInfo.classList.add('file-info');
                       
                        // 創建label和textbox
                        const label = document.createElement('strong');
                        label.textContent = '檔名: '; // 用textContent避免注入
                        fileInfo.appendChild(label);
                       
                        const input = document.createElement('input');
                        input.type = 'text';
                        input.value = file.name; // 初始值為檔名，用value設定，安全無注入風險
                        input.classList.add('file-name-input'); // 可以加CSS類別來樣式化
                        fileInfo.appendChild(input);
                       
                        // 如果你要顯示大小，可以加在下面（隱藏或顯示）
                        // const sizeInfo = document.createElement('br');
                        // fileInfo.appendChild(sizeInfo);
                        // const sizeLabel = document.createElement('strong');
                        // sizeLabel.textContent = '大小: ';
                        // fileInfo.appendChild(sizeLabel);
                        // const sizeText = document.createTextNode(file.size + ' bytes');
                        // fileInfo.appendChild(sizeText);
                       
                        fileItem.appendChild(fileInfo);
                        // 新增刪除按鈕（垃圾桶樣子）
                        const deleteBtn = document.createElement('button');
                        deleteBtn.classList.add('delete-btn');
                        deleteBtn.innerHTML = '\u2716'; // 垃圾桶 X
                        deleteBtn.addEventListener('click', () => {
                            fileItem.remove();
                            // 移除對應的storedFiles項目
                            storedFiles = storedFiles.filter(item => item.input !== input);
                            // 如果所有項目都刪除，恢復初始文字
                            if (dropZone.children.length === 0) {
                                dropZone.innerHTML = '<p>將檔案或元素拖到這裡</p>';
                            }
                        });
                        fileItem.appendChild(deleteBtn);
                        dropZone.appendChild(fileItem);
                       
                        // 儲存檔案和input
                        storedFiles.push({ originalFile: file, editedNameInput: input });
                    }
                } else {
                    // 非檔案資料
                    const data = event.dataTransfer.getData('text');
                    if (data) {
                        dropZone.innerHTML = `${data}`;
                    }
                }
            });

            function uploadFiles() {
                const address = document.getElementById('<%= AddressInput.ClientID %>').value;
                if (!address || storedFiles.length === 0) {
                    alert('請輸入地址並拖入檔案！');
                    return false;
                }

                const formData = new FormData();
                formData.append('address', address); // 傳地址給後端解析

                storedFiles.forEach((item, index) => {
                    const editedName = item.editedNameInput.value.trim() || item.originalFile.name;
                    const newFile = new File([item.originalFile], editedName, { type: item.originalFile.type });
                    formData.append('files[]', newFile);
                });

                fetch('/UploadPagesFile/UploadHandler.ashx', {
                    method: 'POST',
                    body: formData
                })
                .then(response => response.text())
                .then(result => {
                    alert(result);
                    // 清空
                    storedFiles = [];
                    dropZone.innerHTML = '<p>將檔案或元素拖到這裡</p>';
                })
                .catch(error => {
                    alert('上傳失敗： ' + error);
                });
                
                return false; // 防止任何 form 提交
            }
        </script>
       
        <div class="Zone">
            <!-- address --><!-- placeholder 提示文字 -->
            <asp:TextBox ID="AddressInput" CssClass="input" autocomplete="off" runat="server" placeholder="input the address"></asp:TextBox>
            <asp:Button ID="AddressConfirmButton" CssClass="btn" runat="server" Text="Confirm" OnClientClick="return uploadFiles();" />
        </div>
    </form>
</body>
</html>