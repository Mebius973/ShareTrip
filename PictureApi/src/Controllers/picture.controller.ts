import { Controller, Post, UploadedFile, UseInterceptors } from '@nestjs/common';
import { FileInterceptor } from '@nestjs/platform-express';
import { diskStorage } from 'multer';
import { extname } from 'path';
import * as fs from 'fs';
import * as ExifParser from 'exif-parser';
import { PictureService } from "src/Services/picture.service";
import { PictureEntity } from 'src/Entities/PictureEntity';

interface PictureUploadResult {
    message: string
    filename: string
    path: string
}

@Controller()
export class PictureController {
    constructor(private readonly photoService: PictureService) {}

    @Post("upload")
    @UseInterceptors(FileInterceptor('file', {
        storage: diskStorage({
        destination: './uploads',
        filename: (req, file, callback) => {
            const uniqueSuffix = Date.now() + '-' + Math.round(Math.random() * 1e9);
            const ext = extname(file.originalname);
            const filename = `${uniqueSuffix}${ext}`;
            callback(null, filename);
        }
        })
    }))
    uploadPicture(@UploadedFile() file: Express.Multer.File): PictureUploadResult {
        const buffer = fs.readFileSync(file.path);
        const parser = ExifParser.create(buffer);
        let metadata = {};

        try {
        const result = parser.parse();
            metadata = result.tags; // contient les tags exif : GPS, DateTime, etc.
        } catch (err) {
            console.warn('Pas de metadata EXIF trouvée');
        }

        const entity = new PictureEntity(file.filename, file.path, metadata)

        this.photoService.savePictureInfo(entity)

        return {
            message: 'File uploaded successfully',
            filename: file.filename,
            path: file.path
        }
    }
}