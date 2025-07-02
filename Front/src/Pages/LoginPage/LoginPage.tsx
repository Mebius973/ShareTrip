import { useState } from 'react';
import { UserService } from '../../Services/UserService';
import './LoginPage.css';

function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const service = new UserService()

  const handleLogin = (e: React.FormEvent) => {
    e.preventDefault();
    // 👉 Appelle ton service ici
    service.login(email, password)
  };

  return (
    <div className="login-page">
      <h1 className="login-title">Connexion</h1>
      <form className="login-form" onSubmit={handleLogin}>
        <input
          type="email"
          placeholder="Adresse email"
          value={email}
          onChange={e => setEmail(e.target.value)}
          required
        />
        <input
          type="password"
          placeholder="Mot de passe"
          value={password}
          onChange={e => setPassword(e.target.value)}
          required
        />
        <button type="submit">Se connecter</button>
      </form>
    </div>
  );
}

export default LoginPage;
