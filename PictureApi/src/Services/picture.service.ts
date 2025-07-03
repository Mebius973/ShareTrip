import { Injectable } from "@nestjs/common";
import { PictureRepositoryImpl } from "src/Data/picture.repositoryImpl";
import { PictureEntity } from "src/Entities/PictureEntity";

@Injectable()
export class PictureService {
    constructor(private readonly pictureRepository: PictureRepositoryImpl) {}

    async savePictureInfo(picture: PictureEntity) {
        await this.pictureRepository.savePictureInfoAsync(picture)
    }
}