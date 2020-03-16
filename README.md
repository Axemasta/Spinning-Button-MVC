# Spinning-Button-MVC
An ASP.NET MVC demo for having spinning buttons on downloads!

![Screenshot of the site](/assets/Main%20Site.png)

## Setup

- Pull Code
- Open Solution
- Edit the following web config values:
  - DownloadFilePath
    - ie `c:\temp\file.txt`
  - DownloadFileName
    - ie `file.txt`
  - DownloadFileType
    - mime type ie `text/plain` ([see more mime types](https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types))
- Build & Run



## Tests



### 1 - Spinning Example

This is just demonstrating the button spinner. Click the button and it will spin for 2 seconds.

#### Before

![Test 1 Before](/assets/Test%201%20Before.png)

#### After

![Test 1 After](/assets/Test%201%20After.png)



### 2 - Direct Download

This test download the file directly from the MVC controller using `FileContentResult`:

```c#
public async Task<ActionResult> DirectDownload()
{
    ...
    return File(fileBytes, _downloadFileType, _downloadFileName);
}
```



#### Before

![Test 2 Before](/assets/Test%202%20Before.png)

#### After

The biggest problem is that calling the download this way means the button spinner cannot be added. The user has no idea what is happening (during my simulated 2 second delay).

![Test 2 After](/assets/Test%202%20After.png)

The file will download from the browser after a short delay.



### 3 - Spinning Download

This test calls the MVC controller via AJAX, this means I have full control over the whole API call and can perform the button spin handling.



An extra caveat is that the controller must return the result as `Json(...)` otherwise the `FileContentResult` won't return any of the right information (it returns the file contents as a `UTF8` encoded string).



```c#
public async Task<ActionResult> IndirectDownload()
{
	...
    var file = File(fileBytes, _downloadFileType, _downloadFileName);
    return Json(file, JsonRequestBehavior.AllowGet);
}
```

Once you have the result back in the javascript world, you need to manually reconstruct the file download and create an `<a>` tag to click to trigger the download:

```javascript
var bytes = new Uint8Array(response.FileContents);
var blob = new Blob([bytes], { type: response.ContentType });
var link = document.createElement('a');
link.href = window.URL.createObjectURL(blob);
link.download = response.FileDownloadName;
link.click();
```

#### Before

![Test 3 Before](/assets/Test%203%20Before.png)

#### After

![Test 3 After](/assets/Test%203%20After.png)

The file will download from the browser after a short delay. The spinner is present whilst the user is waiting and removed once the file has been downloaded, perfect! 😄