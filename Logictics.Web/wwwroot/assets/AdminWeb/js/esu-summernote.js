function initSummernoteUI(destinationElement) {
    destinationElement.summernote({
        //height: 600,
        //fontNames: ["Noto Sans JP", "Times New Roman"],
        disableDragAndDrop: true,
        spellCheck: true,
        disableGrammar: false,
        toolbar: [
            ['font', ['bold', 'italic', 'underline']],
            ['color', ['forecolor']],
            ['misc', ['undo', 'redo']],
            ['view', ['fullscreen', 'codeview']],
        ],
        onPaste: function (e) {
            var bufferText = ((e.originalEvent || e).clipboardData || window.clipboardData).getData('Text');
            e.preventDefault();
            document.execCommand('insertText', false, bufferText);
        },
    });
}

function initSummernoteCustomUI(destinationElement, componentButton) {
    destinationElement.summernote({
        //height: 600,
        //fontNames: ["Noto Sans JP", "Times New Roman"],
        disableDragAndDrop: true,
        spellCheck: true,
        disableGrammar: false,
        toolbar: [
            ['font', ['bold', 'italic', 'underline']],
            ['color', ['forecolor']],
            ['customButton', ['componentButton']],
            ['misc', ['undo', 'redo']],
            ['view', ['fullscreen', 'codeview']],
        ],
        buttons: {
            componentButton,
        },
        onPaste: function (e) {
            var bufferText = ((e.originalEvent || e).clipboardData || window.clipboardData).getData('Text');
            e.preventDefault();
            document.execCommand('insertText', false, bufferText);
        },
    });
}



function initSummernoteCustomUI(destinationElement, audioButton, templateButton) {
    destinationElement.summernote({
        //height: 600,
        fontNames: ["Noto Sans JP", "Times New Roman", "San Francisco"],
        fontNamesIgnoreCheck: ["Noto Sans JP", "Times New Roman", "San Francisco"], // web fonts to be ignored
        disableDragAndDrop: true,
        spellCheck: true,
        disableGrammar: false,
        toolbar: [
            ['myTemplateButton', ['templateBTN']],
            ['font', ['bold', 'italic', 'underline', 'fontname']],
            ['color', ['forecolor']],
            ['myAudioButton', ['audioBTN']],
            ['misc', ['undo', 'redo']],
            ['view', ['fullscreen', 'codeview']],
        ],
        buttons: {
            audioBTN: audioButton,
            templateBTN: templateButton,
        },
        onPaste: function (e) {
            var bufferText = ((e.originalEvent || e).clipboardData || window.clipboardData).getData('Text');
            e.preventDefault();
            document.execCommand('insertText', false, bufferText);
        },
    });
}

// read a template file
function getTemplateContent(templatePath) {
    // load template file
    var host = window.location.origin;
    var url = host + templatePath;
    var content = loadXMLDoc(url);
    // load template content to summernote
    return content;
}

// read a template file
function initSummernoteContent(templatePath, destinationElement) {
    // load template file
    var host = window.location.origin;
    var url = host + templatePath;
    var content = loadXMLDoc(url);
    // load template content to summernote
    destinationElement.summernote('code', content);
}

function loadXMLDoc(theURL) {
    var xmlhttp;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari, SeaMonkey
        xmlhttp = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhttp.open("GET", theURL, false);
    xmlhttp.send();
    return xmlhttp.responseText;
}

// support
function convertStringToHTML(htmlString) {
    var parser = new DOMParser();
    var doc = parser.parseFromString(htmlString, 'text/html');
    return doc.body.textContent;
}