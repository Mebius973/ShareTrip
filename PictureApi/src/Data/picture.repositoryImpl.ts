import { Injectable } from "@nestjs/common";
import { Picture, PictureDocument } from "./Schemas/Picture";
import { InjectModel } from "@nestjs/mongoose";
import { Model } from "mongoose";
import { PictureEntity } from "src/Entities/PictureEntity";

@Injectable()
export class PictureRepositoryImpl {
    constructor(@InjectModel(Picture.name) private pictureModel: Model<PictureDocument>) {}

    async savePictureInfoAsync(picture: PictureEntity): Promise<boolean> {
        const createdPicture = new this.pictureModel(picture);
        const savedPicture = await createdPicture.save();
        return savedPicture != null
    }
}