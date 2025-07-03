import { NestFactory } from '@nestjs/core';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';
import { join } from 'path';
import * as express from 'express';
import { AppModule } from './app.module';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  // Configuration de base de Swagger
  const config = new DocumentBuilder()
    .setTitle('Picture API')
    .setDescription('API pour gérer les photos de voyage et leurs commentaires')
    .setVersion('1.0')
    // .addBearerAuth() // si tu utilises JWT ou autre auth
    .build();

  // Création du document Swagger
  const document = SwaggerModule.createDocument(app, config);

  // Endpoint pour accéder à la doc Swagger UI
  SwaggerModule.setup('swagger', app, document);


  app.use('/uploads', express.static(join(__dirname, '..', 'uploads')));
  await app.listen(process.env.PORT ?? 3000);
}
bootstrap();
