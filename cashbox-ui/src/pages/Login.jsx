import { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate(); 

  const handleLogin = async (e) => {
    e.preventDefault(); 
    try {
      const API_URL = "https://localhost:44339/api/User/loginUser"; 
      const response = await axios.post(API_URL, { email, password });

      if (response.data && response.data.accessToken) {
          localStorage.setItem("token", response.data.accessToken);
          alert("Giriş Başarılı!");
          
          navigate('/dashboard'); 
      }
    } catch (error) {
      alert("Hata: Giriş başarısız!");
    }
  };

  return (
    <div style={{ padding: '50px', maxWidth: '400px', margin: '0 auto', fontFamily: 'sans-serif' }}>
      <h2 style={{ textAlign: 'center', color: '#333' }}>CashBox'a Giriş Yap</h2>
      
      <form onSubmit={handleLogin} style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
        
        <div>
          <label style={{ fontWeight: 'bold' }}>E-posta</label>
          <input 
            type="email" 
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="ornek@mail.com"
            required
            style={{ width: '100%', padding: '10px', marginTop: '5px', borderRadius: '5px', border: '1px solid #ccc' }}
          />
        </div>

        <div>
          <label style={{ fontWeight: 'bold' }}>Şifre</label>
          <input 
            type="password" 
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="******"
            required
            style={{ width: '100%', padding: '10px', marginTop: '5px', borderRadius: '5px', border: '1px solid #ccc' }}
          />
        </div>

        <button 
          type="submit" 
          style={{ padding: '10px', backgroundColor: '#007bff', color: 'white', border: 'none', borderRadius: '5px', cursor: 'pointer', fontSize: '16px', fontWeight: 'bold' }}>
          Giriş Yap
        </button>
        
      </form>
    </div>
  );
}

export default Login;