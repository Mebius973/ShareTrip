import { useState } from 'react';
import './RegisterPage.css';

function RegisterPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirm, setConfirm] = useState('');
  const [errors, setErrors] = useState<string[]>([]);

  const validatePassword = (pwd: string): string[] => {
    const rules = [
      { regex: /.{8,}/, message: 'Au moins 8 caractères' },
      { regex: /[a-z]/, message: 'Au moins une minuscule' },
      { regex: /[A-Z]/, message: 'Au moins une majuscule' },
      { regex: /[^A-Za-z0-9]/, message: 'Au moins un caractère spécial' },
    ];

    return rules.filter(rule => !rule.regex.test(pwd)).map(r => r.message);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const pwdErrors = validatePassword(password);
    const mismatch = password !== confirm ? ['Les mots de passe ne correspondent pas'] : [];

    const allErrors = [...pwdErrors, ...mismatch];
    setErrors(allErrors);

    if (allErrors.length === 0) {
      console.log('Inscription avec', email, password);
      // 👉 Appelle ton service ici
    }
  };

  return (
    <div className="register-page">
      <h1>Créer un compte</h1>
      <form onSubmit={handleSubmit}>
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
        <input
          type="password"
          placeholder="Confirmation"
          value={confirm}
          onChange={e => setConfirm(e.target.value)}
          required
        />
        <button type="submit">S’inscrire</button>
      </form>

      {errors.length > 0 && (
        <ul className="error-messages">
          {errors.map((err, i) => (
            <li key={i}>{err}</li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default RegisterPage;
