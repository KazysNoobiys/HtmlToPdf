import React, {ChangeEvent, Component, useEffect, useRef, useState} from "react";
import './main.css';
import { TailSpin } from "react-loader-spinner";

export enum FileStatus {
    none = 0,
    uploaded,
    processed
}
export type FileDto = {
    id: string
    name: string
    status: FileStatus
}
async function getFiles(): Promise<FileDto []> {
    const url = "http://localhost:5175/api/file/GetFiles";
    const res = await fetch(url, {});
    const data = await res.json();
    return data;
}

function getStatusStr(status: number): string {
    if (status == 1)
        return "Uploaded"
    if (status == 2)
        return "Processed"

    return "nan";
}

export function MainPage() {
    const [files, setFiles] = useState<FileDto[]>([]);
    const [file, setFile] = useState<File>();
    const [isLoading, setIsLoading] = useState<boolean>();
    useEffect(() => {
        async function loadData() {
            const files = await getFiles();
            setFiles(files);
        }

        loadData();
    }, []);
    const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
        if (e.target.files) {
            setFile(e.target.files[0]);
        }
    };
    async function uploadHtml(file: File | undefined) {
        if (!file || isLoading) {
            return;
        }
        setIsLoading(true);
        const url = "http://localhost:5175/api/file/UploadFile";
        const formData = new FormData()
        formData.append("file", file)
        const res = await fetch(url,         {
            "headers": {
                "accept": "*/*",
            },
            "body": formData,
            "method": "POST",
        })
        console.log(res);
        const data = await res.json();

        setFiles([
            ...files,
            data
        ])
        console.log(data);
        setIsLoading(false);
    }
    async function btnDownloadOnClick(id: string, fileName: string) {
        if (isLoading){
            return;
        }
        setIsLoading(true);
        const url = "http://localhost:5175/api/file/GetPdfFile?" + new URLSearchParams({
            id
        });
        const data = await fetch(url).then(r => r.blob());

        const urlPdf = URL.createObjectURL(data);
        const a = document.createElement("a");
        a.download = fileName.slice(0, fileName.lastIndexOf(".")) + ".pdf"
        a.href = url
        document.body.appendChild(a)
        a.click()
        URL.revokeObjectURL(url)
        setIsLoading(false);
    }

    return (
        <div className="main">
            <input
                disabled={isLoading}
                className="inputHtmlFile"
                onChange={handleFileChange} type="file"/>
            <button
                disabled={isLoading}
                className="uploadHtmlBtn"
                onClick={() => uploadHtml(file)}>Upload html file</button>
            {isLoading ?(<TailSpin color="red" radius={"8px"} />) : (
                <div>
                    {files.map((value, idx) => (
                        <div className="row" key={idx}>
                            <label>Name: {value.name}; Status:{getStatusStr(value.status)}</label>
                            <button
                                disabled={isLoading}
                                className="btn"
                                onClick={() => btnDownloadOnClick(value.id, value.name)}> Download Pdf</button>
                        </div>
                    ))}
                </div>
            )}

        </div>
    );
}