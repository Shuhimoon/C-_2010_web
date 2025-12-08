<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadPage.aspx.cs" Inherits="MyWorkWebsite.UploadPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Page</title>
    <link href="Styles/SubPage.css" rel="Stylesheet" />
    <link rel="icon" type="image/x-icon" href="Styles/cat.png" />
    <!--[if IE]><link href="IE_SubPage/IESubPage.css" type="text/css" rel="Stylesheet"/><![endif]-->
</head>
<body>
    <form id="form1" runat="server">
    <div id="Zone">
        <div id="dropZone">
            <p>將檔案或元素拖到這裡</p>
        </div>
        <script>
            const dropZone = document.getElementById('dropZone');

            // 防止瀏覽器預設行為
            dropZone.addEventListener('dragover', (event) => {
                event.preventDefault();
                dropZone.classList.add('dragover');
            });

            dropZone.addEventListener('dragleave', () => {
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

                        // 檔名和大 小
                        const fileInfo = document.createElement('div');
                        fileInfo.classList.add('file-info');
                        fileInfo.innerHTML = `<strong>檔名:</strong> ${file.name}<br><strong>大小:</strong> ${file.size} bytes`;

                        fileItem.appendChild(fileInfo);

                        // 新增刪除按鈕（垃圾桶樣子）
                        const deleteBtn = document.createElement('button');
                        deleteBtn.classList.add('delete-btn');
                        deleteBtn.innerHTML = '\u2716'; // 垃圾桶 emoji
                        deleteBtn.addEventListener('click', () => {
                            fileItem.remove();
                            // 如果所有項目都刪除，恢復初始文字
                            if (dropZone.children.length === 0) {
                                dropZone.innerHTML = '<p>將檔案或元素拖到這裡</p>';
                            }
                        });
                        fileItem.appendChild(deleteBtn);

                        dropZone.appendChild(fileItem);
                    }
                } else {
                    // 非檔案資料
                    const data = event.dataTransfer.getData('text');
                    if (data) {
                        dropZone.innerHTML = `${data}`;
                    }
                }
            });
        </script>
    </div>
    </form>
</body>
</html>
