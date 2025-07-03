import { Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { ConfigModule } from '@nestjs/config';
import { PictureController } from './Controllers/picture.controller';
import { PictureService } from './Services/picture.service';
import { Picture, PictureSchema } from './Data/Schemas/Picture';
import { PictureRepositoryImpl } from './Data/picture.repositoryImpl';

@Module({
  imports: [
    ConfigModule.forRoot({ isGlobal: true }),
    MongooseModule.forRoot(process.env.MONGO_URI ?? (() => { throw new Error('MONGO_URI environment variable is not set'); })()),
    MongooseModule.forFeature([{ name: Picture.name, schema: PictureSchema }])
  ],
  controllers: [PictureController],
  providers: [PictureService, PictureRepositoryImpl],
})
export class AppModule {}
